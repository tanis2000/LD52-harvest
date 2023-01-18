using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Damage;
using App.Enemies;
using App.Hero;
using GameBase.SceneChanger;
using GameBase.Utils;
using Unity.Netcode;
using UnityEngine;

namespace App.Rooms
{
    public class WaveSystem : MonoBehaviour
    {
        public EnemySpawnInfo[] EnemySpawnInfo;
        public Transform SpawnEffect;
        public Transform Room;
        public float WaveInterval = 60.0f;
        public int EnemiesLimit = 300;

        private EnemySpawner[] enemySpawners;
        private bool isPaused;

        private void OnEnable()
        {
            enemySpawners = Room.GetComponentsInChildren<EnemySpawner>();
        }

        private void Start()
        {
            if (!NetworkManager.Singleton.IsServer)
            {
                return;
            }

            StartCoroutine(RunWave());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                isPaused = true;
            } else if (Input.GetKeyDown(KeyCode.U))
            {
                isPaused = false;
            }
        }

        private IEnumerator RunWave()
        {
            var random = new XRandom(1337);
            yield return new WaitForSeconds(1f);
            var wave = 1;
            
            while (AtLeastOnePlayerAlive() && !isPaused)
            {
                var enemies = new List<ChaserEnemy>();
                var enemyCount = random.Range(Mathf.Min(wave*10, 100), Mathf.Min(wave*12, 200));
                Debug.Log($"Wave {wave} - enemies {enemyCount}");
                var waveTime = 0.0f;

                var aliveEnemies = enemies.Count(x => x.Health.IsAlive);
                while ((aliveEnemies < enemyCount || waveTime < WaveInterval) && !isPaused)
                {
                    aliveEnemies = enemies.Count(x => x.Health.IsAlive);
                    var numOfEnemiesToSpawn = Mathf.Min(enemyCount - aliveEnemies, EnemiesLimit);
                    if (numOfEnemiesToSpawn > 0)
                    {
                        Debug.Log($"Num of enemies to spawn {numOfEnemiesToSpawn}");
                        var spawnPoints = random.ItemTake(enemySpawners, numOfEnemiesToSpawn);
                        foreach (var spawnPoint in spawnPoints)
                        {
                            var position = spawnPoint.transform.position +
                                           new Vector3(random.Range(-1f, 1f), 0, random.Range(-1f, 1f));

                            if (SpawnEffect != null)
                            {
                                Instantiate(
                                    SpawnEffect,
                                    position,
                                    Quaternion.identity,
                                    transform
                                );
                            }
                
                            var esi = random.Item(EnemySpawnInfo);
                            var enemy = Instantiate(
                                esi,
                                position,
                                Quaternion.identity,
                                transform
                            ).GetComponent<ChaserEnemy>();
                            enemy.Target = null;
                            enemies.Add(enemy);
                            var h = enemy.GetComponent<Health>();
                            h.Amount = h.Max.GetRandom();
                            var no = enemy.GetComponent<NetworkObject>();
                            no.Spawn(true);
                        }
                    }

                    waveTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                
                wave++;
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
    }
}