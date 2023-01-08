using System;
using System.Collections.Generic;
using App.Damage;
using App.Hero.Actions;
using GameBase.Animations;
using UnityEngine;

namespace App.Hero
{
    public class ScytheAttackAction : HeroAction
    {
        public float Cooldown = 2;
        public float BaseDamage = 10;
        public DamageArea DamageArea;
        public Transform Bubble;

        private Animator heroAnimator;
        private float cd;
        private static readonly int Swing = Animator.StringToHash("swing");
        private Health health;

        private void OnEnable()
        {
            heroAnimator = GetComponent<Animator>();
            DamageArea = GetComponentInChildren<DamageArea>();
            health = GetComponent<Health>();
            cd = Cooldown;
        }

        private void Update()
        {
            if (Game.Instance.IsPaused())
            {
                return;
            }

            if (!health.IsAlive)
            {
                return;
            }
            
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                heroAnimator.SetBool(Swing, true);
                cd = Cooldown;
                var area = GetBaseArea(DamageArea.BaseRadius);
                DamageArea.Trigger(GetBaseDamage(), area);

                var size = new Vector3(area*2, area*2, area*2);
                Tweener.Instance.ScaleTo(Bubble, size, 0.2f, 0f, TweenEasings.BounceEaseOut);
                Invoke(nameof(HideBubble), 0.2f);
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

        private float GetBaseArea(float baseArea)
        {
            var res = baseArea;
            foreach (var powerUp in PowerUps)
            {
                res += baseArea * powerUp.BaseAreaIncPercentage;
            }

            return res;
        }

        private void HideBubble()
        {
            Tweener.Instance.ScaleTo(Bubble, Vector3.zero, 0.1f, 0f, TweenEasings.QuadraticEaseOut);
        }
    }
}