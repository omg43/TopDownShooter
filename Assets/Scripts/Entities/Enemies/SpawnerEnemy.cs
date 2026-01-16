using Entities.Enemies.Data;
using UnityEngine;

namespace Entities.Enemies
{
    internal class SpawnerEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyData[] m_data;
        [SerializeField] private Enemy[] m_enemies;

        [SerializeField] private Transform[] m_spawnPoints;
        [SerializeField] private Transform m_playerTransfrom;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            foreach (var spawnPoint in m_spawnPoints)
            {
                var enemy = GetEnemy();
                var enemyData = GetEnemyData();

                var enemyInstance = Instantiate(enemy, spawnPoint);
                enemyInstance.Initialize(enemyData, spawnPoint);

                enemyInstance.Died += OnDied;
            }
        }

        private void OnDied(Enemy enemy)
        {
            enemy.Died -= OnDied;
            Destroy(enemy.gameObject);
        }

        private Enemy GetEnemy() => m_enemies[Random.Range(0, m_enemies.Length)];
        private EnemyData GetEnemyData() => m_data[Random.Range(0, m_data.Length)];
    }
}
