using System;
using System.Collections.Generic;
using App.Hero.Actions;
using UnityEngine;

namespace App.Hero.PowerUps
{
    public class PowerUpContainer: MonoBehaviour
    {
        public List<PowerUp> AvailablePowerUps;
        public List<PowerUp> ObtainedPowerUps;
        public HeroAction HeroAction;
        public string PowerUpName;

        public PowerUp GetNextPowerUp()
        {
            if (ObtainedPowerUps.Count == AvailablePowerUps.Count)
            {
                return null;
            }

            foreach (var availablePowerUp in AvailablePowerUps)
            {
                if (!ObtainedPowerUps.Contains(availablePowerUp))
                {
                    return availablePowerUp;
                }
            }

            return null;
        }
        
        public void AddNextPowerUp()
        {
            if (ObtainedPowerUps.Count == AvailablePowerUps.Count)
            {
                return;
            }

            foreach (var availablePowerUp in AvailablePowerUps)
            {
                if (!ObtainedPowerUps.Contains(availablePowerUp))
                {
                    ObtainedPowerUps.Add(availablePowerUp);
                    HeroAction.AddPowerUp(availablePowerUp);
                    break;
                }
            }
        }

        public int NumberOfAvailablePowerUps()
        {
            return AvailablePowerUps.Count - ObtainedPowerUps.Count;
        }

    }
}