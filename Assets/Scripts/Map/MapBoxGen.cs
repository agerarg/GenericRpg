using System.Collections;
using UnityEngine;

public class MapBoxGen : MonoBehaviour
{
    // 45 15.5 -45
    public GameObject WallPrefab;
    public GameObject GoldPrefab;
    public GameObject MonsterPrefab;
    public Transform WallHoleder;
    public float xGap = 10f;
    public float zGap = 10f;
    public string MapKey="";
    Vector3 WallPosition;
    GameObject NewWall;

    public MapPiller[] mapThingsToSpown = new MapPiller[100];

    private PlayerMove playerMove;
    private float checkPlayerFarAway=0;
    void ResetWallPosition()
    {
        WallPosition = new Vector3(45f, 15.5f, -45f);
    }
    void Start()
    {
        playerMove = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
    }
    public void StartGeneration()
    {
        for(int i=0;i<100;i++)
        {
            mapThingsToSpown[i] = GetWallExact();
        }
        GenMapWalls();
    }
    public void GenerationFrom(MapPiller[] pillars)
    {
        mapThingsToSpown = pillars;
        GenMapWalls();
    }
    MapPiller GetWallExact()
    {
        int num = Random.Range(0, 3);
        if (num == 0)
            num = 1;
        else
            num = 0;

        if(33 == Random.Range(0,50))
            num = 2;
        int lifeGive = 0;
        int monsters = 0;
        switch(num)
        {
            case 1: //Wall
                lifeGive = 10;
            break;
            case 2: //Gold
                lifeGive = 25;
            break;
        }
        //Monster Spown
        if(num==0 && 3== Random.Range(0, 5))
        {
            monsters = Random.Range(0, 3);
            num = 3; // SpownMonsters
        }

        MapPiller MP = new MapPiller
        {
            identity = num,
            life = lifeGive,
            monsterCount = monsters
        };

        return MP;
    }

    void GenMapWalls()
    {
        ResetWallPosition();
        int mapThingIndex = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (mapThingsToSpown[mapThingIndex].identity == 1)
                {
                    NewWall = Instantiate(WallPrefab, Vector3.zero, Quaternion.identity, WallHoleder);
                    NewWall.transform.localPosition = WallPosition;
                }
                if (mapThingsToSpown[mapThingIndex].identity == 2)
                {
                    NewWall = Instantiate(GoldPrefab, Vector3.zero, Quaternion.identity, WallHoleder);
                    NewWall.transform.localPosition = WallPosition;
                }
                if (mapThingsToSpown[mapThingIndex].identity == 3)
                {
                    MonsterPrefab = Instantiate(MonsterPrefab, Vector3.zero, Quaternion.identity, WallHoleder);
                    MonsterPrefab.transform.localPosition = WallPosition;
                }
                WallPosition = new Vector3(WallPosition.x + xGap, WallPosition.y, WallPosition.z);
                mapThingIndex++;
            }
            WallPosition = new Vector3(45f, 15.5f, WallPosition.z -= zGap);
        }
    }
    IEnumerator WaitToSpown()
    {
        yield return new WaitForSeconds(1);
        
    }
    void LateUpdate()
    {
        if (checkPlayerFarAway > 5f)
        {
            float dist = Vector3.Distance(playerMove.GetPlayerPosition(), transform.position);
            if (dist > 200f)
            {
                ProceduralMapGrowth.instance.SaveMap(MapKey);
                ProceduralMapGrowth.instance.RemoveFromListMap(MapKey);
                Destroy(gameObject);
            }
            checkPlayerFarAway = 0;
        }
        checkPlayerFarAway += Time.deltaTime;
    }
}

