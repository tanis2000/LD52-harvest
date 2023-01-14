using System;
using UnityEngine;

namespace App.GameState
{
    public enum GameState
    {
        MainMenu,
        Main
    }
    
    public abstract class GameStateBehaviour : MonoBehaviour
    {
        public virtual bool Persists
        {
            get { return false; }
        }

        protected virtual void Start()
        {
            if (Persists)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}