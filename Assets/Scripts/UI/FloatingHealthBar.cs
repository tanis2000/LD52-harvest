using App.Damage;
using UnityEngine;

namespace App.UI
{
    public class FloatingHealthBar : MonoBehaviour
    {
        public Texture2D Texture;
        private Health health;

        private void OnEnable()
        {
            health = GetComponentInParent<Health>();
        }

        private void OnGUI()
        {
            var width = 25;
            var height = 6;
            var border = 1;
            var position = Camera.main.WorldToScreenPoint(transform.position);

            var bgRect = new Rect(
                position.x - width / 2 - border,
                Screen.height - position.y - border,
                width + border * 2,
                height + border * 2);

            GUI.color = Color.black * 0.5f;
            GUI.DrawTexture(bgRect, Texture);

            var barRect = new Rect(
                position.x - width / 2,
                Screen.height - position.y,
                width * (health.Amount / health.Max),
                height
            );

            GUI.color = Color.white;
            GUI.DrawTexture(barRect, Texture);
        }
    }
}