using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{
    public class ApplicationController: MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 120;
            SceneManager.LoadScene("MainMenu");
        }
    }
}