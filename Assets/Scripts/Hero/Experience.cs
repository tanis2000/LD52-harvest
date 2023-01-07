using System;
using App.Hero.PowerUps;
using UnityEngine;

namespace App.Hero
{
    public class Experience : MonoBehaviour
    {
        public int Amount = 0;
        public int Max = 5;

        private Level level;
        private PowerUpChoice powerUpChoice;
        
        private void OnEnable()
        {
            level = GetComponent<Level>();
            powerUpChoice = FindObjectOfType<PowerUpChoice>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LevelUp();
            }
            
            if (Amount >= Max)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Max += 10;
            Amount = 0;
            level.Amount++;
            Game.Instance.Pause();
            ShowPowerUps();
        }

        private void ShowPowerUps()
        {
            powerUpChoice.CreateList();
            powerUpChoice.Show();
        }
    }
}