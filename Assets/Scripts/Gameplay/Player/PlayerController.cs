using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Weapons.Standard;
using Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerContainer;
        [SerializeField] private Animator animator;
        [SerializeField] private AbstractWeapon weapon;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float rotationAngle = 30f;
        [SerializeField] private float speed = 6f;
        private readonly int _movementAnimatorKeyHash = Animator.StringToHash("Movement");
        private readonly int _startShootAnimationKeyHash = Animator.StringToHash("StartShoot");
        private readonly int _stopShootAnimationKeyHash = Animator.StringToHash("StopShoot");
        private InputService _inputService;
        private Coroutine _moveToCoroutine;
        private bool _isWalking;

        public bool IsWalking => _isWalking;

        private void Start()
        {
            _inputService.MouseDownObservable.Subscribe(Shoot).AddTo(this);
        }


        [Inject]
        private void Inject(InputService inputService)
        {
            _inputService = inputService;
        }

        private void Shoot(Vector3 position)
        {
            weapon.Shoot();

            if (_isWalking)
            {
                return;
            }
            AnimateShoot(true);
        }

        public void MoveTo(Vector3 position, Action onCompleted = null)
        {
            if (_moveToCoroutine!= null)
            {
                StopCoroutine(_moveToCoroutine);
            }
            AnimateShoot(false);
            Debug.Log("Move to: " + position);
            _moveToCoroutine = StartCoroutine(MoveToRoutine(position, onCompleted));
            Debug.Log("Move to: " + position + " completed");
        }
       
        private IEnumerator MoveToRoutine(Vector3 position, Action onCompleted)
        {
            playerContainer.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Vector3 direction = (position - transform.position).normalized;
            direction.y = 0f;
            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
            AnimateMovement(1);
        
            WaitForFixedUpdate waitForFixedUpdate = new();
            _isWalking = true;
            while (Vector3.Distance(position, transform.position) > 0.05f)
            {
                rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
                yield return waitForFixedUpdate;
                direction = (position - transform.position).normalized;
            }
        
            _isWalking = false;
            AnimateMovement(0);
            playerContainer.transform.localRotation = Quaternion.Euler(Vector3.up * -rotationAngle);
            onCompleted?.Invoke();
        }

        private void AnimateMovement(float movement)
        {
            animator.SetFloat(_movementAnimatorKeyHash, movement);
        }

        private void AnimateShoot(bool isShooting)
        {
            if (isShooting)
            {
                animator.SetTrigger(_startShootAnimationKeyHash);
                return;
            }
            animator.SetTrigger(_stopShootAnimationKeyHash);
        }
    }
}