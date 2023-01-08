using System.Collections.Generic;
using GameBase.Utils;
using TMPro;
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
            DestroyUtils.DestroyChildren(Wrapper);
            var createdButtons = 0;
            foreach (var container in PowerUpContainers)
            {
                if (container.NumberOfAvailablePowerUps() > 0)
                {
                    CreateButton(container);
                    createdButtons++;
                }
            }

            if (createdButtons == 0)
            {
                Hide();
                Game.Instance.Resume();
            }
        }

        public void CreateButton(PowerUpContainer container)
        {
            var go = Instantiate(ButtonPrefab, Wrapper);
            var btn = go.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() =>
            {
                container.AddNextPowerUp();
                Hide();
                Game.Instance.Resume();
            });
            var text = go.GetComponentInChildren<TMP_Text>();
            var nextPowerUp = container.GetNextPowerUp();
            text.text = $"XXX\n+{nextPowerUp.BaseDamageIncPercentage*100}% DAMAGE\n+{nextPowerUp.BaseAreaIncPercentage*100}% AREA";
        }
    }
}