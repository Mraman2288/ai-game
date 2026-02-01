using UnityEngine;

namespace OrbitCourier
{
    public class RestartOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                LevelSession.Instance?.RestartLevel();
            }
        }
    }
}
