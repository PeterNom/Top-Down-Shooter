using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public GameObject Menu;
    private bool GamePaused;
    private PlayerActions _menuActions;
    private float timer = 0.0f;
    private float waitZone = 2.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        GamePaused = true;
        Time.timeScale = GamePaused ? 0 : 1;
        Menu.SetActive(GamePaused);
        _menuActions = new PlayerActions();
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

    bool getGameState()
    { 
        return GamePaused; 
    }
}
