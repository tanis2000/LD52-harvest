using GameBase.Animations;
using Unity.Netcode;
using UnityEngine;

namespace App.Hero
{
    /// <summary>
    /// <see cref="ClientCharacter"/> is responsible for displaying a character on the client's screen based on state information sent by the server.
    /// </summary>
    public class ClientCharacter : NetworkBehaviour
    {
        private ServerCharacter serverCharacter;
        private CameraFollow cameraFollow;
        
        public override void OnNetworkSpawn()
        {
            serverCharacter = GetComponent<ServerCharacter>();
            if (!serverCharacter.CharacterClass.IsNpc)
            {
                if (serverCharacter.IsOwner)
                {
                    cameraFollow = FindObjectOfType<CameraFollow>();
                    cameraFollow.Target = transform;
                }
            }
        }
    }
}