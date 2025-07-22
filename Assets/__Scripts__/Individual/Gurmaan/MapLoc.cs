using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MapLoc : MonoBehaviour
{
    public List<GameObject> FutureLoc;
    public SpriteRenderer Icon;
    public SpriteRenderer Circle;
    public MapEvent mapEvent;
    
    private bool clickable = false;
    private MapManager mapManager;
    private GameSceneManager sceneManager;

    void Start()
    {
        mapManager = GameObject.FindWithTag("PManager").GetComponent<MapManager>();
        sceneManager = Camera.main.GetComponent<GameSceneManager>();
        Circle.enabled=false;
        clickable = false;
    }
    
    void Update()
    {
        if (mapManager.PlayerLoc == this.name)
        {
            Circle.enabled = true;
            clickable = false;
            foreach (GameObject obj in FutureLoc)
            {
                obj.GetComponent<MapLoc>().Circle.enabled = true;
                obj.GetComponent<MapLoc>().clickable = true;
            }
        }
    }
    
    void OnMouseDown()
    {
        if(clickable)
        {
            mapManager.PlayerLoc = name;
            sceneManager.OpenEvent(mapEvent.ToString());
        }
    }
}
public enum MapEvent{
    Butcher,
    Verdant_Lands,
    Battle,
    Battle_Lv2,
    Battle_Lv3,
    Battle_Lv4,
    Boss,
    Card_Choice,
    Monopoly,
    Industry_Expansion,
    Natures_Respite,
    Null
}