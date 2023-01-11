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
            //TODO: turn this around and make sure there is a script on the text component that looks for the player level component and updates the value
            //LevelText.text = $"{Amount}";
        }

        public void Increase()
        {
            Amount++;
            //LevelText.text = $"{Amount}";
        }
    }
}