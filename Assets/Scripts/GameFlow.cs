using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameFlow : MonoBehaviour
{
    public SaveManager saveManager;
    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject vase;
    private Damageable damageablePlayer;
    private Damageable damageableVase;
    public GameObject gameOverPanel;
    public float positionSpawnChange = 0.5f;

    public int phase1EnemyCount = 5;
    public int phase2EnemyCount = 10;
    public int phase3EnemyCount = 12;

    public int currentPhase = 1;
    private bool phaseStarted = false;


    // Start is called before the first frame update
    void Awake()
    {
        damageablePlayer = player.GetComponent<Damageable>();
        damageableVase = vase.GetComponent<Damageable>();
    }
    void Start()
    {

        if (GameManager.gameState == GameManager.GameState.NewGame)
        {

        }
        else if (GameManager.gameState == GameManager.GameState.Continue)
        {
            GameData data = saveManager.LoadGame();
            if (data != null)
            {
                GameManager.score = data.score;
                currentPhase = data.phase;
                player.transform.position = new Vector3(data.playerPosition_x, data.playerPosition_y, 0);
                damageablePlayer.CurrentHealth = data.playerHealth;
            }
        }


        StartPhase(currentPhase);
        GameManager.ResetScore();
    }

    public int GetCurrentPhase()
    {
        return currentPhase;
    }

    private void StartPhase(int currentPhase)
    {
        phaseStarted = true;
        int enemies = phase1EnemyCount += 2;
        SpawnEnemies(enemies);
        Debug.Log(enemies);
    }

    void Update()
    {
        if (phaseStarted && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextPhase();
        }

        if (!damageablePlayer.IsAlive || !damageableVase.IsAlive)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private async void SpawnEnemies(int phaseEnemyCount)
    {
        for (int i = 0; i < phaseEnemyCount; i++)
        {
            if (Random.Range(0f, 1f) <= positionSpawnChange)
            {
                Vector3 spawnPos = new Vector3(-10, 5, 0);
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
                Instantiate(enemyPrefab, spawnPos, spawnRotation);
                await Task.Delay(2500);
            }
            else
            {
                Vector3 spawnPos = new Vector3(12, 4, 0);
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
                Instantiate(enemyPrefab, spawnPos, spawnRotation);
                await Task.Delay(2500);
            }

        }
    }


    private void NextPhase()
    {
        currentPhase++;

        if (currentPhase <= 50)
        {
            StartPhase(currentPhase);
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

}
