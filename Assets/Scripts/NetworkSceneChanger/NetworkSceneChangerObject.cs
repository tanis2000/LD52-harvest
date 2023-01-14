using GameBase.Utils;
using UnityEngine;

namespace App.NetworkSceneChanger
{


    public class NetworkSceneChangerObject : MonoBehaviour
    {
        public void ChangeScene(string scene)
        {
            NetworkSceneChanger.Instance.ChangeScene(scene);
        }

        public void Quit()
        {
            NetworkSceneChanger.Instance.Blinders.Close();
            this.StartCoroutine(Application.Quit, 1f);
        }

        public void ToMainOrName()
        {
            ChangeScene(PlayerPrefs.HasKey("PlayerName") ? "Main" : "Name");
        }
    }
}