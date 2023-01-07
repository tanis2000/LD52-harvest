using App.Damage;
using GameBase.Utils;
using UnityEngine;

namespace App.Hero
{
    public class MovementAction : MonoBehaviour
    {
        public float Acceleration = 100f;
        public float MaxSpeed = 9f;
        public float Damp = 10f;
        public float DirectionChangeMultiplier = 2f;
        public Vector3 LastMove { get; private set; }
        public LayerMask GroundMask;

        private CapsuleCollider capsuleCollider;
        private Health health;
        private Rigidbody body;
        private Vector3 movement = Vector3.zero;
        private readonly Collider[] groundOverlaps = new Collider[5];

        private void OnEnable()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            health = GetComponentInParent<Health>();
            body = GetComponentInParent<Rigidbody>();
        }

        private void Move()
        {
            movement = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                movement.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement.x = 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movement.z = -1;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                movement.z = 1;
            }
        }

        private void FixedUpdate()
        {
            if (Game.Instance.IsPaused())
            {
                return;
            }

            // Return if the player cannot act
            if (!health.IsAlive)
            {
                return;
            }

            Move();

            // Apply damping
            var dampedVelocity = GameBase.Utils.Utils.Damp(body.velocity.FlatY(), 1f / 60f / Damp, Time.fixedDeltaTime);
            dampedVelocity.y = body.velocity.y;
            body.velocity = dampedVelocity;

            // Apply gravity if not standing on ground
            var radius = capsuleCollider.radius;
            var size = Physics.OverlapSphereNonAlloc(transform.position + Vector3.up * (radius * 0.85f), radius,
                groundOverlaps, GroundMask, QueryTriggerInteraction.Ignore);
            var grounded = size > 0;
            if (!grounded)
            {
                if (body.velocity.y > -10f)
                    body.velocity += Physics.gravity * Time.fixedDeltaTime;
            }
            else
            {
                body.velocity = body.velocity.FlatY();
            }

            // Debug.DrawRay(transform.position, Vector3.right * 10, grounded ? Color.red : Color.white);

            // Return if not trying to move
            if (movement.magnitude < 0.01f)
            {
                // Set our velocity to zero to stop moving suddenly
                body.velocity = new Vector3(0, body.velocity.y, 0);
                return;
            }

            // Clamp diagonal movement
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            // Calculate a factor that relates the current speed to the max speed
            // Close to max ->  [0..1] <- Far from max 
            // Describing the amount of acceleration should be added to reach the max speed
            var factor = Mathf.Clamp01((MaxSpeed - body.velocity.magnitude) / MaxSpeed);

            // Give extra acceleration if you are changing direction
            var directionFactor = -Mathf.Min(0f, Vector3.Dot(movement.normalized, body.velocity.FlatY().normalized));
            factor += directionFactor * DirectionChangeMultiplier;

            // Apply the velocity
            body.velocity += Acceleration * movement * factor * Time.fixedDeltaTime;
        }
    }
}