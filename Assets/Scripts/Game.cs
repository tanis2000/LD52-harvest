using System;
using GameBase.Audio;
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
            PlayMainTheme();
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

        public void PlayMainTheme()
        {
            AudioSystem.Instance().Play("SoundTheme1");
        }
    }
}