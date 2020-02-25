using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class ProceduralMapGrowth : MonoBehaviour
{
    public static ProceduralMapGrowth instance;

    public Dictionary<string, MapPiller[]> MapsDictionary = new Dictionary<string, MapPiller[]>();
    public MapBoxGen MapBoxPrefab;
    public GameObject MapBasicPrefab;
    private PlayerMove playerMove;
    private float timeToCheckPlayerPosition;

    private int gridPositionX = 0;
    private int gridPositionZ = 0;
    private Vector3 NewMapPos;
    private bool isGenerationGoing = false;
    void Awake()
    {
        instance = this;
        playerMove = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
    }
     void Start()
    {
        GenerateMapsArraund(0, 0);
    }
    private bool LoadMap(string key)
    {
        string destination = Application.persistentDataPath + "/mapData/" + key + ".map";
        FileStream file;

        if (File.Exists(destination))
            file = File.OpenRead(destination);
        else
            return false;

        BinaryFormatter bf = new BinaryFormatter();
        MapBlock data = (MapBlock)bf.Deserialize(file);

        MapsDictionary.Add(key, data.pillars);

        file.Close();
        return true;
    }
    public void RemoveFromListMap(string key)
    {
        MapsDictionary.Remove(key);
    }
    //When a map is deleted will save data
    public void SaveMap(string key)
    {
       string path = Application.persistentDataPath + "/mapData";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string destination = Application.persistentDataPath + "/mapData/"+ key + ".map";
        FileStream file;
        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);
        //Saving Data
        MapBlock data = new MapBlock();
        data.pillars = MapsDictionary[key];
        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    void CreateBlock(int X,int Z)
    {
        if (CheckBaseHome(X, Z))
        {
            NewMapPos = new Vector3(X * 100, 0, Z * 100);
            Collider[] hitColliders = Physics.OverlapSphere(NewMapPos, 5f);
            int i = 0;
            bool isEmptySpace = true;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "MapBox" || hitColliders[i].tag == "Base")
                    isEmptySpace = false;
                i++;
            }
            if (isEmptySpace)
            {
                string PositionKey = X + "t" + Z;
                if (MapsDictionary.ContainsKey(PositionKey))
                {
                    MapBoxGen map = Instantiate(MapBoxPrefab, NewMapPos, Quaternion.identity, transform);
                    map.GenerationFrom(MapsDictionary[PositionKey]);
                    map.MapKey = PositionKey;
                }
                else
                {
                    if (LoadMap(PositionKey))
                    {
                        MapBoxGen map = Instantiate(MapBoxPrefab, NewMapPos, Quaternion.identity, transform);
                        map.GenerationFrom(MapsDictionary[PositionKey]);
                        map.MapKey = PositionKey;
                    }
                    else
                    {
                        MapBoxGen map = Instantiate(MapBoxPrefab, NewMapPos, Quaternion.identity, transform);
                        map.StartGeneration();
                        MapsDictionary.Add(PositionKey, map.mapThingsToSpown);
                        map.MapKey = PositionKey;
                    }

                }
            }
        }
    }
    bool CheckBaseHome(int X, int Z)
    {
        if (X == 0 && Z == 0)
        {
            if (!playerMove.HaveHouse())
            {
                GameObject home = Instantiate(MapBasicPrefab, NewMapPos, Quaternion.identity, transform);
                playerMove.SetHome(home);
            }
            else
            {
                playerMove.GetHouse().SetActive(true);
            }
            return false;
        }
        else
            return true;
    }
    void GenerateMapsArraund(int X,int Z)
    {

        NewMapPos = new Vector3(gridPositionX * 100, 0, gridPositionZ * 100);
        
        //CENTER
        CreateBlock(X, Z);

        //SIDES
        CreateBlock((X - 1), Z);
        CreateBlock((X + 1), Z);
        CreateBlock((X), (Z + 1));
        CreateBlock((X), (Z - 1));

        //CORNERS
        CreateBlock((X -1), (Z - 1));
        CreateBlock((X + 1), (Z + 1));
        CreateBlock((X - 1), (Z + 1));
        CreateBlock((X + 1), (Z - 1));

        gridPositionX = X;
        gridPositionZ = Z;

        isGenerationGoing = false;
    }

   void CheckMyGrid()
    {
        Vector3 PlayerPos = playerMove.GetPlayerPosition();
        int newGridPosX = (int)(PlayerPos.x / 100);
        int newGridPosZ = (int)(PlayerPos.z / 100);
        if(newGridPosX!=gridPositionX || newGridPosZ!= gridPositionZ)
        {
            isGenerationGoing = true;
            GenerateMapsArraund(newGridPosX, newGridPosZ);
        }
    }
    void Update()
    {
  
        if(timeToCheckPlayerPosition>1)
        {

            if(!isGenerationGoing)
            CheckMyGrid();
            timeToCheckPlayerPosition = 0;

        }
        timeToCheckPlayerPosition += Time.deltaTime;
      
    }

    
}
