using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    static int total = 0;
    static int index = 0;

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
        Destroy(gameObject);
    }
}
