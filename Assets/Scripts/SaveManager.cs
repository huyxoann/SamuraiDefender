using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath = "save.json";

    public GameObject player;
    private Damageable damageable;
    public GameFlow gameFlow;


    void Awake()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<Damageable>();
    }

    public void SaveGame(){
        GameData data = new GameData();
        
        data.playerPosition_x = player.transform.position.x;
        data.playerPosition_y = player.transform.position.y;

        data.playerHealth = damageable.CurrentHealth;

        data.score = GameManager.score;

        data.phase = gameFlow.currentPhase;

        string json = JsonConvert.SerializeObject(data);
        File.WriteAllTextAsync(saveFilePath, json);
        Debug.Log(json);

    }
    public void LoadGame(){
        if(File.Exists(saveFilePath)){
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonConvert.DeserializeObject<GameData>(json);
            Debug.Log(data.score);
        }else{
            Debug.Log("No save file found!");
        }
    }
}