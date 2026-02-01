using UnityEngine;

namespace OrbitCourier
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Launch")]
        public float launchPower = 10f;
        public float maxDragDistance = 2.5f;

        [Header("Pulse")]
        public int maxPulses = 3;
        public float pulseForce = 3f;

        [Header("References")]
        public Transform aimPivot;

        private Rigidbody2D rb;
        private bool hasLaunched;
        private int pulsesLeft;
        private Vector2 dragStartWorld;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            pulsesLeft = maxPulses;
        }

        private void Update()
        {
            if (!hasLaunched)
            {
                HandleLaunchInput();
            }
            else
            {
                HandlePulseInput();
            }
        }

        private void HandleLaunchInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStartWorld = GetPointerWorld();
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 current = GetPointerWorld();
                Vector2 drag = Vector2.ClampMagnitude(dragStartWorld - current, maxDragDistance);
                if (aimPivot != null)
                {
                    aimPivot.position = (Vector2)transform.position + drag;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 current = GetPointerWorld();
                Vector2 drag = Vector2.ClampMagnitude(dragStartWorld - current, maxDragDistance);
                rb.velocity = Vector2.zero;
                rb.AddForce(drag * launchPower, ForceMode2D.Impulse);
                hasLaunched = true;
                LevelSession.Instance?.RegisterLaunch();
            }
        }

        private void HandlePulseInput()
        {
            if (pulsesLeft <= 0)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
            {
                Vector2 direction = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : Vector2.up;
                rb.AddForce(direction * pulseForce, ForceMode2D.Impulse);
                pulsesLeft--;
                LevelSession.Instance?.RegisterPulse();
            }
        }

        private Vector2 GetPointerWorld()
        {
            Vector3 screen = Input.mousePosition;
            Vector3 world = Camera.main.ScreenToWorldPoint(screen);
            return world;
        }

        public void ResetPlayer(Vector2 position)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.position = position;
            hasLaunched = false;
            pulsesLeft = maxPulses;
        }

        public int PulsesLeft => pulsesLeft;
    }
}
