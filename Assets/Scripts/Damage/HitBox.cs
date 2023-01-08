using GameBase.Audio;
using UnityEngine;

namespace App.Damage
{
    public class HitBox : MonoBehaviour
    {
        public Health Health;
        private Invulnerability invulnerability;
        //private Bleed bleed;

        private void OnEnable()
        {
            invulnerability = GetComponent<Invulnerability>();
            //bleed = GetComponent<Bleed>();
        }

        public void DoDamage(DamageInfo damageInfo)
        {
            if (invulnerability != null && invulnerability.IsInvulnerable())
            {
                return;
            }
            Health.Modify(damageInfo.Delta);
            Debug.Log(damageInfo.Source.name);
            if (damageInfo.Source.name == "ScytheDamageArea")
            {
                AudioSystem.Instance().Play("SoundHit");
            }
            else
            {
                AudioSystem.Instance().Play("SoundHurt");
            }
            //bleed.DoBleed();
        }
    }
}