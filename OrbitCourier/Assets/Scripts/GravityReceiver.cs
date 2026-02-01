using System.Collections.Generic;
using UnityEngine;

namespace OrbitCourier
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityReceiver : MonoBehaviour
    {
        public List<GravitySource> sources = new List<GravitySource>();

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 totalForce = Vector2.zero;
            foreach (GravitySource source in sources)
            {
                if (source == null)
                {
                    continue;
                }
                totalForce += source.GetForce(rb.position);
            }

            if (totalForce != Vector2.zero)
            {
                rb.AddForce(totalForce, ForceMode2D.Force);
            }
        }
    }
}
