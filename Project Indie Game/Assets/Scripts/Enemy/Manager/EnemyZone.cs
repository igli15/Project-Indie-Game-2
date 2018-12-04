using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour {

    private List<EnemySpawner> m_spawners;


    private int m_numberOfActiveEnemies = 0;
    [Header("Safe to edit next varaibles")]
    public bool m_destroyEnemiesAfterPlayerLeaves = true;
    public bool m_showPopUps = true;
    [Header("Dont edit next varaibles")]
    public bool isZoneCleared = false;
    public bool isPlayerInsideZone = false;
	void Awake () {
        m_spawners = new List<EnemySpawner>();
        //CallNextWave();

    }
	
	void Update () {
        if (isZoneCleared) return;

        if (m_numberOfActiveEnemies <= 0&&isPlayerInsideZone&& m_showPopUps)
        {
            if (currentWaveIndex != -1)
            {
                AchievementPopUp.QueueAchievement("Wave" + (currentWaveIndex + 1));
            }

            CallNextWave();
        }
	}

    public void CallNextWave()
    {

        m_numberOfActiveEnemies = 0;
        foreach (EnemySpawner spawner in m_spawners)
        {
            int temp = spawner.SpawnNextWave();
            if (temp == -1)
            {
                isZoneCleared = true;
                return;
            }
            m_numberOfActiveEnemies += temp;
        }
    }

    public void AddSpawner(EnemySpawner spawner)
    {
        m_spawners.Add(spawner);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        
        if(!isZoneCleared) CallNextWave();
        isPlayerInsideZone = true;

    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        isPlayerInsideZone = false;
       
        foreach (EnemySpawner spawner in m_spawners)
        {
            if(m_destroyEnemiesAfterPlayerLeaves)spawner.DestroyAllMyEnemies();
            spawner.currentWaveIndex--;
        }
        
        for (int i = 0; i < 3; i++)
        {
            AchievementPopUp.ResetAchievement("Wave" + (i+1));
        }
    }

    public int numberOfActiveEnemies
    {
        get { return m_numberOfActiveEnemies; }
        set { m_numberOfActiveEnemies = value; }
    }

    public int currentWaveIndex{
        get{
            return m_spawners[0].currentWaveIndex;
        }
    }
    
}
