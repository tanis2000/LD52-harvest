using System;
using System.Collections.Generic;
using App.Damage;
using App.Hero;
using App.Network;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace App.GameState
{
    public class ServerMainGameState : GameStateBehaviour
    {
        public NetcodeHooks NetcodeHooks;
        public NetworkObject PlayerPrefab;
        public bool InitialSpawnDone;
        
        private bool restarting;

        private void Awake()
        {
            NetcodeHooks.OnNetworkSpawnHook += OnNetworkSpawn;
            NetcodeHooks.OnNetworkDespawnHook += OnNetworkDespawn;
        }
        
        void OnNetworkSpawn()
        {
            if (!NetworkManager.Singleton.IsServer)
            {
                enabled = false;
                return;
            }

            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;
            NetworkManager.Singleton.SceneManager.OnSynchronizeComplete += OnSynchronizeComplete;
        }

        void OnNetworkDespawn()
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnLoadEventCompleted;
            NetworkManager.Singleton.SceneManager.OnSynchronizeComplete -= OnSynchronizeComplete;
        }

        private void OnDestroy()
        {
            if (NetcodeHooks)
            {
                NetcodeHooks.OnNetworkSpawnHook -= OnNetworkSpawn;
                NetcodeHooks.OnNetworkDespawnHook -= OnNetworkDespawn;
            }
        }

        void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {
            if (!InitialSpawnDone && loadSceneMode == LoadSceneMode.Single)
            {
                InitialSpawnDone = true;
                foreach (var kvp in NetworkManager.Singleton.ConnectedClients)
                {
                    SpawnPlayer(kvp.Key, false);
                }
            }
        }
        
        void OnSynchronizeComplete(ulong clientId)
        {
            if (InitialSpawnDone && !PlayerServerCharacter.GetPlayerServerCharacter(clientId))
            {
                //somebody joined after the initial spawn. This is a Late Join scenario. This player may have issues
                //(either because multiple people are late-joining at once, or because some dynamic entities are
                //getting spawned while joining. But that's not something we can fully address by changes in
                //ServerBossRoomState.
                SpawnPlayer(clientId, true);
            }
        }

        
        void SpawnPlayer(ulong clientId, bool lateJoin)
        {
            Transform spawnPoint = null;
            var pos = GetStartingPosition();
            var playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);

            var newPlayer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);

            var newPlayerCharacter = newPlayer.GetComponent<ServerCharacter>();
            newPlayerCharacter.transform.position = pos;

            var persistentPlayerExists = playerNetworkObject.TryGetComponent(out PersistentHero persistentHero);
            Assert.IsTrue(persistentPlayerExists,
                $"Matching persistent PersistentHero for client {clientId} not found!");
            
            // if reconnecting, set the player's position and rotation to its previous state
            if (lateJoin)
            {
                // TODO
            }
            
            // pass name from persistent player to avatar
            // if (newPlayer.TryGetComponent(out NetworkNameState networkNameState))
            // {
            //     networkNameState.Name.Value = persistentHero.NetworkNameState.Name.Value;
            // }

            // spawn players characters with destroyWithScene = true
            newPlayer.SpawnWithOwnership(clientId, true);
        }

        private Vector3 GetStartingPosition()
        {
            return new Vector3(Random.Range(-3f, 3f), 5f, Random.Range(50f, 70f));
        }

        private void Update()
        {
            // Main loop to check for win/game over state
            if (!restarting && !AtLeastOnePlayerAlive())
            {
                // TODO submit the score for each player instead
                var submitScore = FindObjectOfType<SubmitScore>();
                submitScore.SubmitFakeScore();
            
                NetworkSceneChanger.NetworkSceneChanger.Instance.ChangeScene("MainMenu");

                DeactivateAllPlayers();
                restarting = true;
            }

        }
        
        private bool AtLeastOnePlayerAlive()
        {
            var serverCharacters = PlayerServerCharacter.GetPlayerServerCharacters();
            foreach (var serverCharacter in serverCharacters)
            {
                var health = serverCharacter.GetComponent<Health>();
                if (health.IsAlive)
                {
                    return true;
                }
            }
            return false;
        }

        private void DeactivateAllPlayers()
        {
            var serverCharacters = PlayerServerCharacter.GetPlayerServerCharacters();
            if (serverCharacters == null)
            {
                return;
            }
            
            foreach (var serverCharacter in serverCharacters.ToArray())
            {
                serverCharacter.gameObject.SetActive(false);
            }
        }

    }
}