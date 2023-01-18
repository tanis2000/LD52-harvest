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
        private ClientHeroAvatar clientHeroAvatar;

        private void OnEnable()
        {
            ClientHeroAvatar.LocalClientSpawned += RegisterHeroAvatar;
            ClientHeroAvatar.LocalClientDespawned += DeregisterHeroAvatar;
        }

        private void Update()
        {
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
        
        void RegisterHeroAvatar(ClientHeroAvatar clientAvatar)
        {
            clientHeroAvatar = clientAvatar;
            invulnerabilityAction = clientHeroAvatar.GetComponent<InvulnerabilityAction>();
        }

        void DeregisterHeroAvatar()
        {
            clientHeroAvatar = null;
            invulnerabilityAction = null;
        }

    }
}