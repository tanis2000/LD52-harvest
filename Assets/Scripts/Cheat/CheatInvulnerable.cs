using System;
using App.Hero;
using App.Hero.Actions;
using Unity.Netcode;
using UnityEngine;

namespace App.Cheat
{
    public class CheatInvulnerable : MonoBehaviour
    {
        private InvulnerabilityAction invulnerabilityAction;
        private ServerCharacter serverCharacter;
        
        private void OnEnable()
        {
        }

        private void Update()
        {
            if (serverCharacter == null)
            {
                serverCharacter = PlayerServerCharacter.GetPlayerServerCharacter(NetworkManager.Singleton.LocalClientId);
                if (serverCharacter != null && invulnerabilityAction == null)
                {
                    invulnerabilityAction = serverCharacter.GetComponent<InvulnerabilityAction>();
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (invulnerabilityAction.IsInvulnerable)
                {
                    invulnerabilityAction.SetInvulnerability(false);
                    Debug.Log("Cheat Invulnerability disabled");
                }
                else
                {
                    invulnerabilityAction.SetInvulnerability(true);
                    Debug.Log("Cheat Invulnerability enabled");
                }
            }
        }
    }
}