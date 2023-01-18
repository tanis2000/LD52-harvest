using System;
using App.Hero.PowerUps;
using Unity.Netcode;
using UnityEngine;

namespace App.Hero
{
    public class Experience : MonoBehaviour
    {
        public int Amount = 0;
        public int Max = 5;

        private Level level;
        private PowerUpChoice powerUpChoice;
        private NetworkObject localPlayerObject;
        private int numberOfLevelsToProcess = 0;
        
        private void OnEnable()
        {
            level = GetComponent<Level>();
            powerUpChoice = FindObjectOfType<PowerUpChoice>();
            localPlayerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L) && localPlayerObject.IsLocalPlayer && Application.isEditor)
            {
                AddToLevelToProcess();
            }
            
            if (Amount >= Max && localPlayerObject.IsLocalPlayer)
            {
                LevelUp();
            }

            if (numberOfLevelsToProcess > 0 && !powerUpChoice.IsShowing())
            {
                ShowPowerUps();
                numberOfLevelsToProcess--;
            }
        }

        private void AddToLevelToProcess()
        {
            numberOfLevelsToProcess++;
        }
        
        private void LevelUp()
        {
            Max += 10;
            Amount = 0;
            level.Increase();
            numberOfLevelsToProcess++;
            //Game.Instance.Pause();
        }

        private void ShowPowerUps()
        {
            powerUpChoice.CreateList();
            powerUpChoice.Show();
        }
    }
}