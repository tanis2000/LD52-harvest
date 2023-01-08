using System;
using TMPro;
using UnityEngine;

namespace App.Hero
{
    public class Level : MonoBehaviour
    {
        public int Amount;
        public TMP_Text LevelText;

        private void OnEnable()
        {
            LevelText.text = $"{Amount}";
        }

        public void Increase()
        {
            Amount++;
            LevelText.text = $"{Amount}";
        }
    }
}