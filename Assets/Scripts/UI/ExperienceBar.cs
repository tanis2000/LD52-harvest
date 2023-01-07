using System;
using App.Damage;
using App.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class ExperienceBar : MonoBehaviour
    {
        public Texture2D Texture;
        public Experience Experience;
        public Slider Slider;

        private void Update()
        {
            Slider.value = (float)Experience.Amount / (float)Experience.Max;
        }

    }
}