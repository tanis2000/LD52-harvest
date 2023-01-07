using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Enemies;
using GameBase.Utils;
using UnityEngine;

namespace App.Rooms
{
    public class WaveSystem : MonoBehaviour
    {
        public EnemySpawnInfo[] EnemySpawnInfo;
        public Transform SpawnEffect;
        public Transform Hero;
        public Transform Room;
    
        private EnemySpawner[] enemySpawners;

        private void OnEnable()
        {
            enemySpawners = Room.GetComponentsInChildren<EnemySpawner>();
        }

        private IEnumerator Start()
        {
            var random = new XRandom(1337);
            yield return new WaitForSeconds(1f);

            var waves = 3;
            for (var i = 0; i < waves; i++)
            {
                var enemies = new List<ChaserEnemy>();
                var enemyCount = 3;
                var spawnPoints = random.ItemTake(enemySpawners, enemyCount);

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
                }

                while (enemies.Any(e => e.Health.IsAlive))
                {
                    yield return null;
                }
            }
        }
    }
}