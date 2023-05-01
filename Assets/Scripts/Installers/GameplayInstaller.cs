using Cinemachine;
using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayController gameplayControllerPrefab;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private Camera gameplayCamera;

        public override void InstallBindings()
        {
            Container.Bind<GameplayController>().FromInstance(gameplayControllerPrefab).AsSingle().NonLazy();
            Container.Bind<CinemachineVirtualCamera>().FromInstance(cinemachineVirtualCamera).NonLazy();
            Container.Bind<Camera>().FromInstance(gameplayCamera).NonLazy();
        }
    }
}