using App.Hero.Actions;
using GameBase.Audio;
using UnityEngine;

namespace App.Damage
{
    public class HitBox : MonoBehaviour
    {
        public Health Health;
        private Invulnerability invulnerability;
        private InvulnerabilityAction invulnerabilityAction;
        //private Bleed bleed;

        private void OnEnable()
        {
            invulnerability = GetComponent<Invulnerability>();
            invulnerabilityAction = GetComponent<InvulnerabilityAction>();
            //bleed = GetComponent<Bleed>();
        }

        public void DoDamage(DamageInfo damageInfo)
        {
            if (invulnerabilityAction != null && invulnerabilityAction.IsInvulnerable)
            {
                return;
            }
            
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