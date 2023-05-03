using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Gameplay.GameplayStates
{
    public class FinishState : BaseState
    {
        public override void Enter()
        {
            RestartAsync();
        }

        private async void RestartAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}