using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointGoalModel : MonoBehaviour
    {
        public event Action OnGoalAchieved;

        public void InvokeGoalAchievement()
        {
            OnGoalAchieved?.Invoke();
        }
    }
}