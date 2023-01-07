using System.Collections;
using App.Damage;
using UnityEngine;

namespace App.Enemies
{
    public class ChaserEnemy : MonoBehaviour
    {
        public Transform Target;
        public float Speed = 5;
        public Transform DespawnEffect;
        public Transform Drop;
        public Health Health => health;
        public float SightRange = 5;
        public LayerMask TargetLayerMask;
        public float SightWaitTime = 1.0f;
        public float RoamSpeed = 4;
        public float RandomRange = 5;

        private CharacterController characterController;
        private Health health;
        private Collider[] sightResults = new Collider[1];
        private SubmitScore submitScore;

        private void OnEnable()
        {
            characterController = GetComponent<CharacterController>();
            health = GetComponent<Health>();
            submitScore = FindObjectOfType<SubmitScore>();
        }


        private IEnumerator Start()
        {
            while (health.IsAlive)
            {
                while (Game.Instance.IsPaused())
                {
                    yield return new WaitForFixedUpdate();
                }
                
                yield return new WaitForFixedUpdate();
                if (Target == null && health.IsAlive)
                {
                    yield return Wander();
                }

                if (Target != null && health.IsAlive)
                {
                    var distance = (Target.position - transform.position).magnitude;
                    if (Mathf.Abs(distance) > 1f)
                    {
                        var direction = (Target.position - transform.position).normalized;
                        characterController.Move(direction * Speed * Time.deltaTime);
                        transform.forward = direction;
                    }
                }
            }

            if (DespawnEffect != null)
            {
                Instantiate(
                    DespawnEffect,
                    transform.position,
                    Quaternion.identity,
                    null
                );
            }

            if (Drop != null)
            {
                Instantiate(
                    Drop,
                    transform.position,
                    Quaternion.identity,
                    null
                );
            }

            submitScore.IncrementScore(1);
            Destroy(gameObject);
        }

        private IEnumerator FindTargetInSight()
        {
            var count = Physics.OverlapSphereNonAlloc(transform.position, SightRange, sightResults, TargetLayerMask);
            if (count > 0)
            {
                SetTarget(sightResults[0].transform);
            }

            yield return null;
        }
        
        private IEnumerator Wander()
        {
            var p = transform.position + RandomRange * Random.insideUnitSphere;
            p.y = transform.position.y;
            var timeout = 3f;
            while (Vector3.Distance(p, transform.position) > 1f && timeout > 0)
            {
                timeout -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
                var direction = (p - transform.position).normalized;
                characterController.Move(direction * RoamSpeed * Time.fixedDeltaTime);
                transform.forward = direction;
                yield return FindTargetInSight();
            }

            yield return null;
        }

        private void SetTarget(Transform t)
        {
            Target = t;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, SightRange);
        }
    }
}