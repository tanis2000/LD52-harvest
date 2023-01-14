using App.RuntimeDataContainers;
using Unity.Netcode;
using UnityEngine;

namespace App.Hero
{
    public class PersistentHero : NetworkBehaviour
    {
        public PersistentHeroRuntimeCollection PersistentHeroRuntimeCollection;
        
        public override void OnNetworkSpawn()
        {
            name = "PersistentHero" + OwnerClientId;
            
            // Note that this is done here on OnNetworkSpawn in case this NetworkBehaviour's properties are accessed
            // when this element is added to the runtime collection. If this was done in OnEnable() there is a chance
            // that OwnerClientID could be its default value (0).
            PersistentHeroRuntimeCollection.Add(this);
            
            if (IsServer)
            {
                // var sessionPlayerData = SessionManager<SessionPlayerData>.Instance.GetPlayerData(OwnerClientId);
                // if (sessionPlayerData.HasValue)
                // {
                //     var playerData = sessionPlayerData.Value;
                //     m_NetworkNameState.Name.Value = playerData.PlayerName;
                //     if (playerData.HasCharacterSpawned)
                //     {
                //         m_NetworkAvatarGuidState.AvatarGuid.Value = playerData.AvatarNetworkGuid;
                //     }
                //     else
                //     {
                //         m_NetworkAvatarGuidState.SetRandomAvatar();
                //         playerData.AvatarNetworkGuid = m_NetworkAvatarGuidState.AvatarGuid.Value;
                //         SessionManager<SessionPlayerData>.Instance.SetPlayerData(OwnerClientId, playerData);
                //     }
                // }
            }
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
            RemovePersistentHero();
        }

        public override void OnNetworkDespawn()
        {
            RemovePersistentHero();
        }

        void RemovePersistentHero()
        { 
            PersistentHeroRuntimeCollection.Remove(this);
            if (IsServer)
            {
                // var sessionPlayerData = SessionManager<SessionPlayerData>.Instance.GetPlayerData(OwnerClientId);
                // if (sessionPlayerData.HasValue)
                // {
                //     var playerData = sessionPlayerData.Value;
                //     playerData.PlayerName = m_NetworkNameState.Name.Value;
                //     playerData.AvatarNetworkGuid = m_NetworkAvatarGuidState.AvatarGuid.Value;
                //     SessionManager<SessionPlayerData>.Instance.SetPlayerData(OwnerClientId, playerData);
                // }
            }
        }
    }
}