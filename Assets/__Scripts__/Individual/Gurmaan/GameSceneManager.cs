using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Texture2D cursor;
    private MapManager mapManager;
    public Boolean isCutscene = false;
    public Boolean isTutorial = false; 
       
    void Awake()
    {
        if (isCutscene)
        {
            return;
        }

        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        mapManager = GameObject.FindWithTag("PManager").GetComponent<MapManager>();

        if (isTutorial)
        {
            Tutorial tutorial = FindFirstObjectByType<Tutorial>();
            if (tutorial != null)
            {
                tutorial.StartTutorial();
            }
            else
            {
                Debug.LogWarning("Tutorial script not found in scene.");
            }
        }
    }

    public void OpenEvent(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void Map()
    {
        mapManager.StartCoroutine(mapManager.MapFill());
        SceneManager.LoadScene("Map");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void OpeningCutscene()
    {
        SceneManager.LoadScene("Opening Cutscene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
