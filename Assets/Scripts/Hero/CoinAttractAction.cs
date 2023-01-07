using System;
using App.Props;
using GameBase.Utils;
using UnityEngine;

namespace App.Hero
{
    public class CoinAttractAction : MonoBehaviour
    {
        public float Radius;
        public LayerMask LayerMask;
        public Experience Experience;

        private void Update()
        {
            var colliders = Physics.OverlapSphere(
                transform.position,
                Radius,
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}