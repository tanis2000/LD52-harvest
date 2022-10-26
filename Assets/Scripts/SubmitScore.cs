using GameBase.Leaderboards;
using UnityEngine;

namespace App
{
    public class SubmitScore : MonoBehaviour
    {
        public void SubmitFakeScore()
        {
            ScoreSystem.Instance().SaveScore(PlayerPrefs.GetString("PlayerName"), 100);
        }
    }
}