using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private EnemyData[] m_data;

    private void Spawn()
    {
        foreach(var spawnPoint in m_spawnPoints)
        {
            var enemy = GetEnemy();
            var enemyData = GetEnemyData();

            var enemyInstance = Instantiate(enemy, spawnPoint);
            enemyInstance.Initialize(enemyData);
       

        }
    }

    private Enemy GetEnemy() =>
        m_enemies[Random.Range(0,m_enemies.Length)];
    private EnemyData GetEnemyData() =>
        m_data[Random.Range(0, m_data.Length)];

    public void OnDied()
    {

    }
}
