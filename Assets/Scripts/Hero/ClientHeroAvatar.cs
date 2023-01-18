using System;
using App.RuntimeDataContainers;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Hero
{
    public class ClientHeroAvatar : NetworkBehaviour
    {
        public ClientHeroAvatarRuntimeCollection HeroAvatars;

        public static event Action<ClientHeroAvatar> LocalClientSpawned;

        public static event Action LocalClientDespawned;

        public override void OnNetworkSpawn()
        {
            name = "HeroAvatar" + OwnerClientId;

            if (IsClient && IsOwner)
            {
                LocalClientSpawned?.Invoke(this);
            }

            if (HeroAvatars)
            {
                HeroAvatars.Add(this);
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsClient && IsOwner)
            {
                LocalClientDespawned?.Invoke();
            }

            RemoveNetworkCharacter();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            RemoveNetworkCharacter();
        }

        void RemoveNetworkCharacter()
        {
            if (HeroAvatars)
            {
                HeroAvatars.Remove(this);
            }
        }
    }
}