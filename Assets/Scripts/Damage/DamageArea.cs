using UnityEngine;
using UnityEngine.Serialization;

namespace App.Damage
{
    public class DamageArea : MonoBehaviour
    {
        public float BaseRadius = 3f;
        public LayerMask LayerMask;

        private DamageSource damageSource;

        private void OnEnable()
        {
            damageSource = GetComponent<DamageSource>();
        }

        public void Trigger(float damage, float radius)
        {
            var colliders = Physics.OverlapSphere(
                transform.position,
                radius,
                LayerMask
            );

            foreach (var col in colliders)
            {
                var hitBox = col.GetComponent<HitBox>();
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
            Gizmos.DrawWireSphere(transform.position, BaseRadius);
        }
    }
}