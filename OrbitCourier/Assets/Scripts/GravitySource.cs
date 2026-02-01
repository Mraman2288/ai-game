using UnityEngine;

namespace OrbitCourier
{
    public class GravitySource : MonoBehaviour
    {
        public float gravityStrength = 5f;
        public float gravityRadius = 6f;
        public bool repulsor;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = repulsor ? Color.cyan : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, gravityRadius);
        }

        public Vector2 GetForce(Vector2 targetPosition)
        {
            Vector2 direction = (Vector2)transform.position - targetPosition;
            float distance = direction.magnitude;
            if (distance <= 0.01f || distance > gravityRadius)
            {
                return Vector2.zero;
            }

            float normalized = 1f - (distance / gravityRadius);
            float strength = gravityStrength * normalized;
            Vector2 force = direction.normalized * strength;
            return repulsor ? -force : force;
        }
    }
}
