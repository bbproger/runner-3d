using Gameplay.Pool;
using Gameplay.Weapons.Bullets;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapons.Standard
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [SerializeField] private AbstractBullet bulletPrefab;
        private Pool<AbstractBullet> _bulletPool;
        private Camera _gameplayCamera;

        [Inject]
        private void Inject(Camera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
        }
        private void Start()
        {
            _bulletPool = new Pool<AbstractBullet>(() => Instantiate(bulletPrefab, transform), 10);
        }

        public void Shoot()
        {
            AbstractBullet bullet = _bulletPool.GetObject();
            Ray ray = _gameplayCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 direction;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                direction = (hit.point - transform.position).normalized;
            }
            else
            {
                Debug.Log("No object hit by the ray");
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = _gameplayCamera.transform.position.z;
                Vector3 worldPosition = _gameplayCamera.ScreenToWorldPoint(mousePosition);
                direction = (transform.position - worldPosition).normalized;
            }

            bullet.Initialize(() =>
            {
                bullet.transform.position = transform.position;
                _bulletPool.Consume(bullet);
            });
            bullet.AddForce(direction);
        }
    }
}