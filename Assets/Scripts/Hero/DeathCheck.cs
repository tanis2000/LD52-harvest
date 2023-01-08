using App.Damage;
using GameBase.SceneChanger;
using UnityEngine;

namespace App.Hero
{
    public class DeathCheck : MonoBehaviour
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
                submitScore.SubmitFakeScore();
                SceneChanger.Instance.ChangeScene("MainMenu");
                gameObject.SetActive(false);
            }
        }
    }
}