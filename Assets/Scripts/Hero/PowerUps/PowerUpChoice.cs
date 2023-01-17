using System.Collections.Generic;
using System.Linq;
using App.Hero.Actions;
using GameBase.Utils;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace App.Hero.PowerUps
{
    public class PowerUpChoice : MonoBehaviour
    {
        public Transform Wrapper;
        public List<PowerUpContainer> PowerUpContainers = new List<PowerUpContainer>();
        public Transform ButtonPrefab;
        
        public void Show()
        {
            Wrapper.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Wrapper.gameObject.SetActive(false);
        }

        public void CreateList()
        {
            var serverCharacter = PlayerServerCharacter.GetPlayerServerCharacter(NetworkManager.Singleton.LocalClientId);;
            var actions = serverCharacter.GetComponentsInChildren<HeroAction>();
            
            DestroyUtils.DestroyChildren(Wrapper);
            var createdButtons = 0;
            foreach (var container in PowerUpContainers)
            {
                var correspondingAction = actions.First(x => x.HeroActionType == container.HeroActionType);
                if (container.NumberOfAvailablePowerUps(correspondingAction) > 0)
                {
                    CreateButton(container, correspondingAction);
                    createdButtons++;
                }
            }

            if (createdButtons == 0)
            {
                Hide();
                //Game.Instance.Resume();
            }
        }

        public void CreateButton(PowerUpContainer container, HeroAction heroAction)
        {
            var go = Instantiate(ButtonPrefab, Wrapper);
            var btn = go.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() =>
            {
                container.AddNextPowerUp(heroAction);
                Hide();
                //Game.Instance.Resume();
            });
            var text = go.GetComponentInChildren<TMP_Text>();
            var nextPowerUp = container.GetNextPowerUp(heroAction);
            var desc = "";
            if (nextPowerUp.BaseDamageIncPercentage > 0)
            {
                desc += $" +{nextPowerUp.BaseDamageIncPercentage * 100}% DAMAGE";
            }
            if (nextPowerUp.BaseAreaIncPercentage > 0)
            {
                desc += $" +{nextPowerUp.BaseAreaIncPercentage * 100}% AREA";
            }
            if (nextPowerUp.BaseSpeedIncPercentage > 0)
            {
                desc += $" +{nextPowerUp.BaseSpeedIncPercentage * 100}% MOVEMENT SPEED";
            }
            if (nextPowerUp.BasePickupRangeIncPercentage > 0)
            {
                desc += $" +{nextPowerUp.BasePickupRangeIncPercentage * 100}% PICKUP RANGE";
            }
            text.text = $"{container.PowerUpName}\n{desc}";
        }
    }
}