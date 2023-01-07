using UnityEngine;

namespace App.Damage
{
    public class DamageArea : MonoBehaviour
    {
        public float Radius = 3f;
        public LayerMask LayerMask;

        private DamageSource damageSource;
        private float lastDamage = 0f;

        private void OnEnable()
        {
            damageSource = GetComponent<DamageSource>();
        }

        public void Trigger(float damage)
        {
            var colliders = Physics.OverlapSphere(
                transform.position,
                Radius,
                LayerMask
            );

            foreach (var collider in colliders)
            {
                var hitBox = collider.GetComponent<HitBox>();
                if (hitBox == null)
                {
                    continue;
                }

                var damageInfo = new DamageInfo
                {
                    Source = damageSource,
                    Delta = -damage,
                    HitBox = hitBox
                };

                hitBox.DoDamage(damageInfo);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}