using UnityEngine;

namespace OrbitCourier
{
    public class GoalStation : MonoBehaviour
    {
        public string nextSceneName = "Level2";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LevelSession.Instance?.CompleteLevel(nextSceneName);
            }
        }
    }
}
