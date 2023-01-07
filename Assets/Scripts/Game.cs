using System;
using UnityEngine;

namespace App
{
    public class Game : MonoBehaviour
    {
        private static Game instance;
        private bool isPaused;

        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (Game)FindObjectOfType(typeof(Game));
                }

                return instance;
            }
        }

        private void OnEnable()
        {
            instance = this;
        }

        public bool IsPaused()
        {
            return isPaused;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }
    }
}