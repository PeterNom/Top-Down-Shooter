using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public GameObject Menu;
    public GameObject GameOver;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private bool GamePaused;
    private PlayerActions _menuActions;
    private float timer = 0.0f;
    private float waitZone = 2.0f;
    private int health = 3;
    private int scoreTotal = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        GamePaused = true;
        Time.timeScale = GamePaused ? 0 : 1;
        _menuActions = new PlayerActions();

        Menu.SetActive(GamePaused);
        GameOver.SetActive(false);
    }

    private void OnEnable()
    {
        _menuActions.Menu_Map.Enable();
    }

    private void OnDisable()
    {
        _menuActions.Menu_Map.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_menuActions.Menu_Map.Pause.IsPressed() && timer > waitZone)
        {
            GamePaused = !GamePaused;
            Time.timeScale = GamePaused ? 0 : 1;
            Menu.SetActive(GamePaused);
            timer = 0.0f;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    public void resumeGame()
    {
        if (GamePaused)
        {
            GamePaused = !GamePaused;
            Time.timeScale = GamePaused ? 0 : 1;
            Menu.SetActive(GamePaused);
        }
    }
    
    public void LoadLevel()
    {
        // Use a coroutine to load the Scene in the background
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("First_Level");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void updatePlayerHealth(int amount)
    {
        health += amount;
        
        if (health <= 0)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
        else
        {
            health %= 4;
        }

        healthText.text = "Health: " + health;
    }

    public void updateScore(int score)
    {
        scoreTotal += score;
        scoreText.text = "Score: " + scoreTotal;
    }

    bool getGameState()
    { 
        return GamePaused; 
    }
}
