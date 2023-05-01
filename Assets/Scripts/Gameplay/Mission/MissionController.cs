using System;
using System.Linq;
using Gameplay.Player;
using UniRx;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Mission
    {
        [SerializeField] private EnemiesGroup enemiesGroup;
        [SerializeField] private Transform movePoint;
        [SerializeField] private string id;
        public EnemiesGroup EnemiesGroup => enemiesGroup;
        public Transform MovePoint => movePoint;
        public string Id => id;
    }

    public class MissionController : MonoBehaviour
    {
        [SerializeField] private Mission[] missions;
        private readonly ReactiveCommand _onMissionsCompleteCommand = new();
        private readonly ReactiveCommand<Mission> _onMissionStartedCommand = new();
        private int _currentMissionIndex;
        public IObservable<Unit> OnMissionsCompleteObservable => _onMissionsCompleteCommand.AsObservable();
        public IObservable<Mission> OnMissionStartedObservable => _onMissionStartedCommand.AsObservable();

        public void StartFirstMission()
        {
            StartMission(missions.First().Id);
        }

        private void StartMission(string missionId)
        {
            Debug.Log($"StartMission from MissionController with missionId: {missionId}");
            Mission mission = missions.FirstOrDefault(m => m.Id == missionId);
            if (mission == null)
            {
                Debug.LogError($"Mission with id: {missionId} not found");
                return;
            }

            _onMissionStartedCommand.Execute(mission);
            _currentMissionIndex = Array.IndexOf(missions, mission);
        }

        public void CompleteMission()
        {
            Debug.Log("MissionComplete from MissionController");
            if (_currentMissionIndex == missions.Length - 1)
            {
                _onMissionsCompleteCommand.Execute(Unit.Default);
                return;
            }

            StartMission(missions[_currentMissionIndex + 1].Id);
        }
    }
}