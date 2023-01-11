using Unity.Netcode;
using UnityEngine;

namespace App.Hero
{
    public class Spawner : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Debug.Log($"IsOwner {IsOwner} IsLocalPlayer {IsLocalPlayer}");
            if (IsOwner)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var position = GetStartingPosition();
                transform.position = position;
                Position.Value = position;
            }
            else
            {
                Position.OnValueChanged += (oldValue, newValue) =>
                {
                    Debug.Log($"received position {Position.Value}");
                    transform.position = Position.Value;
                };
                SubmitPositionRequestServerRpc();
            }
        }
        
        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetStartingPosition();
        }

        static Vector3 GetStartingPosition()
        {
            return new Vector3(Random.Range(-3f, 3f), 5f, Random.Range(50f, 70f));
        }

        void Update()
        {
            //transform.position = Position.Value;
        }
    }
}