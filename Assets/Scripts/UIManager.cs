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
    

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();

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
    }

    public void StartNewGame()
    {
        GameManager.gameState = GameManager.GameState.NewGame;
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        GameManager.gameState = GameManager.GameState.Continue;
        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
