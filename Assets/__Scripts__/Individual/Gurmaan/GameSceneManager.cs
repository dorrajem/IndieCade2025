using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Texture2D cursor;
    public void StartGame()
    {
        SceneManager.LoadScene("Battle Scene");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    void Update()
    {
        
    }
}
