using UnityEngine;

namespace OrbitCourier
{
    public class FragmentCollectible : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            LevelSession.Instance?.RegisterFragment();
            Destroy(gameObject);
        }
    }
}
