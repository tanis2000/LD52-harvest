using System.Collections.Generic;
using App.Hero.PowerUps;
using UnityEngine;

namespace App.Hero.Actions
{
    public class HeroAction : MonoBehaviour
    {
        public HeroActionType HeroActionType;
        private List<PowerUp> powerUps = new();

        public List<PowerUp> PowerUps => powerUps;

        public void AddPowerUp(PowerUp powerUp)
        {
            powerUps.Add(powerUp);
        }

        public int NumberOfPowerUps()
        {
            return powerUps.Count;
        }
    }
}