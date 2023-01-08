using System;
using App.Damage;
using GameBase.SceneChanger;
using UnityEngine;

namespace App.Hero
{
    public class DieOnFall : MonoBehaviour
    {
        private Health health;
        private SubmitScore submitScore;

        private void OnEnable()
        {
            health = GetComponent<Health>();
            submitScore = FindObjectOfType<SubmitScore>();
        }

        private void Update()
        {
            if (!health.IsAlive)
            {
                return;
            }
            
            if (transform.position.y < -10)
            {
                health.Modify(-1000);
                submitScore.SubmitFakeScore();
                SceneChanger.Instance.ChangeScene("MainMenu");
            }
        }
    }
}