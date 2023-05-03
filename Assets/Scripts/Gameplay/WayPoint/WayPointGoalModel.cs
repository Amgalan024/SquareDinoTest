using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointGoalModel : MonoBehaviour
    {
        public event Action OnGoalAchieved;
        public event Action OnGoalActivated;

        public bool IsAchieved { get; private set; }
        public bool IsActive { get; private set; }

        public void AchieveGoal()
        {
            IsAchieved = true;
            OnGoalAchieved?.Invoke();
        }

        public void ActivateGoal()
        {
            IsActive = true;
            OnGoalActivated?.Invoke();
        }
    }
}