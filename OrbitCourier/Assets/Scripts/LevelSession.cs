using UnityEngine;
using UnityEngine.SceneManagement;

namespace OrbitCourier
{
    public class LevelSession : MonoBehaviour
    {
        public static LevelSession Instance { get; private set; }

        [Header("Session")]
        public Transform playerSpawn;
        public PlayerController player;
        public float failBoundaryRadius = 15f;

        [Header("Tracking")]
        public float elapsedTime;
        public int pulseCount;
        public int fragmentsCollected;
        public bool launched;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (launched)
            {
                elapsedTime += Time.deltaTime;
            }

            if (player != null && player.transform.position.magnitude > failBoundaryRadius)
            {
                RestartLevel();
            }
        }

        public void RegisterLaunch()
        {
            launched = true;
        }

        public void RegisterPulse()
        {
            pulseCount++;
        }

        public void RegisterFragment()
        {
            fragmentsCollected++;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void CompleteLevel(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
