using System;
using System.Collections.Generic;
using App.Hero.Actions;
using UnityEngine;

namespace App.Hero.PowerUps
{
    public class PowerUpContainer: MonoBehaviour
    {
        public List<PowerUp> AvailablePowerUps;
        // public List<PowerUp> ObtainedPowerUps;
        // public HeroAction HeroAction;
        public HeroActionType HeroActionType;
        public string PowerUpName;

        public PowerUp GetNextPowerUp(HeroAction heroAction)
        {
            if (heroAction.NumberOfPowerUps() == AvailablePowerUps.Count)
            {
                return null;
            }

            foreach (var availablePowerUp in AvailablePowerUps)
            {
                if (!heroAction.PowerUps.Contains(availablePowerUp))
                {
                    return availablePowerUp;
                }
            }

            return null;
        }
        
        public void AddNextPowerUp(HeroAction heroAction)
        {
            if (heroAction.NumberOfPowerUps() == AvailablePowerUps.Count)
            {
                return;
            }

            foreach (var availablePowerUp in AvailablePowerUps)
            {
                if (!heroAction.PowerUps.Contains(availablePowerUp))
                {
                    heroAction.AddPowerUp(availablePowerUp);
                    break;
                }
            }
        }

        public int NumberOfAvailablePowerUps(HeroAction heroAction)
        {
            return AvailablePowerUps.Count - heroAction.NumberOfPowerUps();
        }

    }
}