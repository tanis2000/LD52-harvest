using UnityEngine;

namespace App.Props
{
    public class Coin : MonoBehaviour
    {
        public int Amount = 1;
        public float Speed = 5;

        public void Attract(Transform target)
        {
            transform.position += (target.position - transform.position).normalized * (Speed * Time.deltaTime);
        }
    }
}