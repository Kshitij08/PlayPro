using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score { get; private set; }
    float lastEnemyKilledTime;
    int streakCount;
    float streakExpiryTime = 1;

    private void Start()
    {
        score = 0;
        Enemy.OnDeathStatic += OnEnemykilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    void OnEnemykilled()
    {
        if(Time.time < lastEnemyKilledTime + streakExpiryTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }

        lastEnemyKilledTime = Time.time;

        score += 1 + (int)(Mathf.Pow(1.2f, streakCount));
    }

    void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemykilled;
    }

}
