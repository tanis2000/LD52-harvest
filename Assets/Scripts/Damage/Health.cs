using GameBase.Utils;
using UnityEngine;

namespace App.Damage
{
    public class Health : MonoBehaviour
    {
        public float Amount = 100f;
        public MinMaxRange Max = new MinMaxRange(100f);

        public bool IsAlive => Amount > 0;

        public void Modify(float delta)
        {
            Amount = Mathf.Clamp(Amount + delta, 0f, Max);
        }
    }
}