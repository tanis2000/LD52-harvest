using System;
using System.Collections.Generic;
using App.Damage;
using App.Hero.Actions;
using UnityEngine;

namespace App.Hero
{
    public class ScytheAttackAction : HeroAction
    {
        public float Cooldown = 2;
        public float BaseDamage = 10;
        public DamageArea DamageArea;

        private Animator heroAnimator;
        private float cd;
        private static readonly int Swing = Animator.StringToHash("swing");

        private void OnEnable()
        {
            heroAnimator = GetComponent<Animator>();
            DamageArea = GetComponentInChildren<DamageArea>();
            cd = Cooldown;
        }

        private void Update()
        {
            if (Game.Instance.IsPaused())
            {
                return;
            }
            
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                heroAnimator.SetBool(Swing, true);
                cd = Cooldown;
                DamageArea.Trigger(GetBaseDamage());
            }
            else
            {
                heroAnimator.SetBool(Swing, false);
            }
        }

        private float GetBaseDamage()
        {
            var res = BaseDamage;
            foreach (var powerUp in PowerUps)
            {
                res += BaseDamage * powerUp.BaseDamageIncPercentage;
            }

            return res;
        }

    }
}