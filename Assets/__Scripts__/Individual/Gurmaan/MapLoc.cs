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

    void Start()
    {
        mapManager = GameObject.FindWithTag("PManager").GetComponent<MapManager>();
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
        }
    }
}
public enum MapEvent{
    Butcher,
    Verdant_Lands,
    Battle,
    Card_Choice,
    Monopoly,
    Industry_Expansion,
    Natures_Respite,
    Null
}