using UnityEngine.SceneManagement;

namespace Gameplay.GameplayStates
{
    public class FinishState : BaseGameplayState
    {
        public override void Enter()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}