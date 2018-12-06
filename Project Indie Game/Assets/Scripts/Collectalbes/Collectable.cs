using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour {

    public static int total = 0;
    public static int index = 0;

	void Start () {
        total++;
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("COLLECETED");
        index++;
        AchievementPopUp.AchievementData achievementData = AchievementPopUp.GetAchievement("Coin");
        achievementData.isCompleted = false;
        achievementData.title = "Found maledict symbols";
        achievementData.description = index+"/" + total;
        AchievementPopUp.QueueAchievement("Coin");
	    
	    SceneManager.sceneLoaded += OnSceneLoaded;
	    
        Destroy(gameObject);
    }
	
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		total = 0;
		index = 0;
	}
}
