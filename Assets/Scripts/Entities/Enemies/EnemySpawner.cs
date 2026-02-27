using Entities.Enemies.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData[] m_data;
        [SerializeField] private Enemy[] m_enemies;
        [SerializeField] private Transform[] m_spawnPoints;

        private List<Enemy> m_currentEnemies = new();

        public void DespawnAll()
        {
            foreach(var enemy in m_enemies)
            {
                DestroyEnemy(enemy);
            }
            m_currentEnemies.Clear();
        }

        public void Spawn()
        {
            var playerTransform = ServiceLocator
                .Resolve<PlayerFactory>()
                .Create()
                .transform;

            foreach(var spawnPoint in m_spawnPoints)
            {
                var enemy = GetEnemy();
                var enemyData = GetEnemyData();

                var enemyInstance = Instantiate(enemy, spawnPoint);
                enemyInstance.Initialize(enemyData, playerTransform);

                enemyInstance.Died += OnDied;
                m_currentEnemies.Add(enemy);
            }
        }

        private void OnDied(Enemy enemy)
        {
            DestroyEnemy(enemy);
        }

        private Enemy GetEnemy() =>
            m_enemies[Random.Range(0, m_enemies.Length)];
        
        private EnemyData GetEnemyData() =>
            m_data[Random.Range(0, m_data.Length)];

        public void DestroyEnemy(Enemy enemy)
        {
            enemy.Died -= OnDied;
            Destroy(enemy.gameObject);
        }
    }
}