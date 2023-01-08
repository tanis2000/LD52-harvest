using System;
using App.Hero.Actions;
using App.Props;
using GameBase.Utils;
using UnityEngine;

namespace App.Hero
{
    public class CoinAttractAction : HeroAction
    {
        public float Radius;
        public LayerMask LayerMask;
        public Experience Experience;

        private void Update()
        {
            var colliders = Physics.OverlapSphere(
                transform.position,
                GetPickupRadius(),
                LayerMask
            );

            foreach (var collider in colliders)
            {
                var coin = collider.GetComponent<Coin>();
                if (coin == null)
                {
                    continue;
                }

                coin.Attract(transform);
                if (Vector3.Distance(coin.transform.position, transform.position) < 0.5f)
                {
                    Experience.Amount += coin.Amount;
                    Destroy(coin.gameObject);
                }
            }
        }
        
        private float GetPickupRadius()
        {
            var res = Radius;
            foreach (var powerUp in PowerUps)
            {
                res += Radius * powerUp.BasePickupRangeIncPercentage;
            }

            return res;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}