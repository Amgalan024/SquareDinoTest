using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointGoalModel : MonoBehaviour
    {
        public event Action OnGoalAchieved;

        public bool IsAchieved { get; private set; }

        public void InvokeGoalAchievement()
        {
            IsAchieved = true;
            OnGoalAchieved?.Invoke();
        }
    }
}