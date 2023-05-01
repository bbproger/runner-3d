using System;
using Cinemachine;
using Gameplay.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public enum GameEndReason
    {
        MissionComplete,
        LeaveGame
    }

    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private MissionController missionController;
        [SerializeField] private int cameraFocusedOffset = 10;
        [SerializeField] private int cameraFocusedDistance = 2;
        [SerializeField] private int cameraOffset = -2;
        [SerializeField] private int cameraDistance = 10;
        private CinemachineFramingTransposer _cinemachineFramingTransposer;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private IDisposable _missionStartedDisposable;
        
        private ReactiveCommand<GameEndReason> _onGameEndCommand;
        public IObservable<GameEndReason> OnGameEndObservable => _onGameEndCommand.AsObservable();

        [Inject]
        private void Inject(CinemachineVirtualCamera cinemachineVirtualCamera)
        {
            _cinemachineVirtualCamera = cinemachineVirtualCamera;
        }

        public void StartGame()
        {
            _cinemachineFramingTransposer =
                _cinemachineVirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body)
                    as CinemachineFramingTransposer;
            _onGameEndCommand = new ReactiveCommand<GameEndReason>();

            _onGameEndCommand.AddTo(this);
            missionController.OnMissionsCompleteObservable.Subscribe(_ => EndGame()).AddTo(this);
            missionController.OnMissionStartedObservable.Subscribe(StartMission).AddTo(this);
            _cinemachineVirtualCamera.Follow = playerController.transform;
            missionController.StartFirstMission();
        }

        private void StartMission(Mission mission)
        {
            _missionStartedDisposable =
                mission.EnemiesGroup.OnAllEnemiesDieObservable.Subscribe(_ => CompleteMission());
            playerController.MoveTo(mission.MovePoint.position, ()=>
            {
                SetCameraDistance(cameraFocusedDistance);
                SetCameraOffset(cameraFocusedOffset);
            });
        }

        private void CompleteMission()
        {
            _missionStartedDisposable.Dispose();
            if (playerController.IsWalking)
            {
                SetCameraDistance(cameraDistance);
            }
            else
            {
                SetCameraDistance(cameraDistance);
                SetCameraOffset(cameraOffset);
            }
            missionController.CompleteMission();
        }

        private void SetCameraOffset(int offset)
        {
            Vector3 offsetVector = _cinemachineFramingTransposer.m_TrackedObjectOffset;
            _cinemachineFramingTransposer.m_TrackedObjectOffset.Set(offsetVector.x, offsetVector.y, offset);
        }
        private void SetCameraDistance(int distance)
        {
            _cinemachineFramingTransposer.m_CameraDistance = distance;
        }

        private void EndGame()
        {
            _onGameEndCommand.Execute(GameEndReason.MissionComplete);
        }
    }
}