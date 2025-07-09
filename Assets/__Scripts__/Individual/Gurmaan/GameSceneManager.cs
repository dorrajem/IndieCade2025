using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Map");
    }

    
    void Update()
    {
        
    }
}
