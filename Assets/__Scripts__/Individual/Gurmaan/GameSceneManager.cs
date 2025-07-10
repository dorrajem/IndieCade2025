using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public Texture2D cursor;
    public void StartGame()
    {
        SceneManager.LoadScene("Battle Scene");
    }

    void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    void Update()
    {
        
    }
}
