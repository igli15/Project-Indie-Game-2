using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private float m_spawnRadius = 1f;

    [SerializeField]
    private EnemyZone m_enemyZone;
    [SerializeField]
    private List<Wave> m_waves;
    
    private EnemyManager m_enemyManager;
    private List<Enemy> m_enemies;
    private int m_currentWaveIndex = -1;

    public void Start()
    {
        m_enemies = new List<Enemy>();
        m_enemyManager = EnemyManager.instance;
        m_enemyZone.AddSpawner(this);
    }

    private void Update()
    {
        
    }

    public void SetWaves(List<Wave> waves)
    {
        m_waves = waves;
    }

    public void AddWave(Wave newWave)
    {
        m_waves.Add(newWave);
    }

    public int SpawnNextWave()
    {
        if (m_currentWaveIndex+1 >= m_waves.Count) return -1;
        m_currentWaveIndex++;

        m_enemies.Clear();
        SpawnGoomba(m_waves[m_currentWaveIndex].numberOfGoombas);
        SpawnTurret(m_waves[m_currentWaveIndex].numberOfTurrets);

 
        return m_waves[m_currentWaveIndex].numberOfGoombas + m_waves[m_currentWaveIndex].numberOfTurrets;
    }

    public void SpawnGoomba(int amountOfGoombas = 1)
    {
        for(int i=0;i<amountOfGoombas;i++)
            SpawnEnemy("Goomba");
    }
    public void SpawnTurret(int amountOfTurret= 1)
    {
        for (int i = 0; i < amountOfTurret; i++)
            SpawnEnemy("Turret");
    }

    public void SpawnEnemy(string tag)
    {
        Vector3 spawnPosition;
        Vector3 distnaceToSpawnPosition;
        Vector2 randomCircle = Random.insideUnitCircle.normalized * m_spawnRadius;
        distnaceToSpawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        spawnPosition = distnaceToSpawnPosition + transform.position;

        spawnPosition.y = transform.position.y;

        GameObject newEnemy=ObjectPooler.instance.SpawnFromPool(tag, spawnPosition, transform.rotation);
       
        newEnemy.GetComponent<Enemy>().onEnemyDestroyed += OnMyEnemyDestroyed;
        m_enemies.Add(newEnemy.GetComponent<Enemy>());

        newEnemy.GetComponent<EnemyMovement>().WarpToCurrentPosition();

        newEnemy.GetComponent<EnemyFSM>().ChangeToInitialState();
    }

    public void DestroyAllMyEnemies()
    {
        foreach (Enemy enemy in m_enemies)
        {
            enemy.OnEnemyDestroyed(new Health());
        }
        m_enemies.Clear();
    }

    public void OnMyEnemyDestroyed()
    {
        m_enemyZone.numberOfActiveEnemies--;
    }

    public int currentWaveIndex
    {
        get { return m_currentWaveIndex; }
        set {
            if (value < 0) m_currentWaveIndex = 0;
            else m_currentWaveIndex = value; }
    }
}
