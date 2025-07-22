using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{
    public List<MapEvent> MapEvents;
    public List<MapLoc> MapLocs;
    
    public Dictionary<string, MapEvent> MapStorage = new Dictionary<string, MapEvent>();
    public Dictionary<MapEvent, Sprite> iconPairs = new Dictionary<MapEvent, Sprite>();
    
    [System.Serializable]
    public class EventSpritePair
    {
        public MapEvent mapEvent;
        public Sprite icon;
    }
    public List<EventSpritePair> iconPairsList;
    
    public string PlayerLoc;  
    public static MapManager MapInstance;

    private void Awake()
    {
        if (MapInstance == null)
        {
            MapInstance = this;
            PlayerLoc = "StartLoc";
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            
            Destroy(gameObject);
        }
        foreach (var pair in iconPairsList)
        {
            iconPairs[pair.mapEvent] = pair.icon;
        }
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start Scene")
        {
            MapStorage.Clear();
        }

        if (SceneManager.GetActiveScene().name != "Map")
        {
            MapLocs.Clear();
        }
    }
    public IEnumerator MapFill()
    {
        yield return new WaitForSeconds(0.1f);
        if (SceneManager.GetActiveScene().name == "Map" && MapLocs.Count == 0)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Location"))
            {
                MapLocs.Add(obj.GetComponent<MapLoc>());
            }
        }
        yield return new WaitForSeconds(0.1f);
        
        if (MapStorage.Count==0)
        {
            
            foreach (MapLoc mapLoc in MapLocs)
            {
                if (mapLoc.mapEvent == MapEvent.Null)
                {
                    MapEvent randomEvent= MapEvents[Random.Range(0, MapEvents.Count)];
                    mapLoc.mapEvent = randomEvent;
                }
                mapLoc.Icon.sprite= iconPairs[mapLoc.mapEvent];
                MapStorage.Add(mapLoc.name, mapLoc.mapEvent);
            }
        }
        else
        {
            foreach (string mapLocName in MapStorage.Keys)
            {
                MapLoc mapLoc = GameObject.Find(mapLocName).GetComponent<MapLoc>();
                mapLoc.mapEvent = MapStorage[mapLocName];
                mapLoc.Icon.sprite= iconPairs[mapLoc.mapEvent];
            }
        }
    }
}
