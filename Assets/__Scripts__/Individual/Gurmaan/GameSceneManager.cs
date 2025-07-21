using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Texture2D cursor;
    private MapManager mapManager;
    public Boolean isCutscene = false;
       
    void Awake()
    {
        if (isCutscene)
        {
            return;
        }
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        mapManager = GameObject.FindWithTag("PManager").GetComponent<MapManager>();
    }
    
    public void Battle()
    {
        SceneManager.LoadScene("Battle Scene");
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

    public void Menu()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
