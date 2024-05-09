using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameFlow : MonoBehaviour
{

    public GameObject enemyPrefab;

    public int phase1EnemyCount = 5;
    public int phase2EnemyCount = 10;
    public int phase3EnemyCount = 12;

    private int currentPhase = 1;
    private bool phaseStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        StartPhase(currentPhase);
    }

    private void StartPhase(int currentPhase)
    {
        phaseStarted = true;
        switch (currentPhase)
        {
            case 1:
                SpawnEnemies(phase1EnemyCount);
                Debug.Log("Phase 1 end");
                break;
            case 2:
                SpawnEnemies(phase2EnemyCount);
                Debug.Log("Phase 2 end");
                break;
            case 3:
                SpawnEnemies(phase3EnemyCount);
                Debug.Log("Phase 3 end");
                break;
            default:
                Debug.Log("Invalid phase number");
                break;
        }
    }

    void Update()
    {
        if (phaseStarted && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextPhase();
        }
    }

    private async void SpawnEnemies(int phaseEnemyCount)
    {
        for (int i = 0; i < phaseEnemyCount; i++)
        {
            Vector3 spawnPos = new Vector3(-8, 0.5f, 5);
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
            Instantiate(enemyPrefab, spawnPos, spawnRotation);
            await Task.Delay(2000);
        }
    }

    // Update is called once per frame


    private void NextPhase()
    {
        currentPhase++;

        if (currentPhase <= 3)
        {
            StartPhase(currentPhase);
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

}
