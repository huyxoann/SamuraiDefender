using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    
    AudioManager audioManager;

    public string gameSceneName= "GameScene";
    public string mainSceneName= "MenuScene";

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName==mainSceneName){
            audioManager.PlayMusic(audioManager.mainTheme);
        }else if(currentSceneName==gameSceneName){
            audioManager.PlayMusic(audioManager.battleTheme);
        }
    }

    void Update()
    {
    }

    void OnEnable()
    {
        CharacterEvent.characterDamaged += CharacterTookDamage;
        CharacterEvent.characterHealed += CharacterHealed;
    }
    void OnDisable()
    {
        CharacterEvent.characterDamaged -= CharacterTookDamage;
        CharacterEvent.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {

        // Create text at character hit
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = "-" + damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored)
    {

        // Create text at character hit
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = "+" + healthRestored.ToString();
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        audioManager.PlayMusic(audioManager.battleTheme);
    }

    public void StartNewGame()
    {
        GameManager.gameState = GameManager.GameState.NewGame;
        LoadGameScene();
    }
    public void ContinueGame()
    {
        GameManager.gameState = GameManager.GameState.Continue;
        LoadGameScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
