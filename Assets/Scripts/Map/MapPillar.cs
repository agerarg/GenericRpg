using System;

//Used by class MapBoxGen to create walls with data.
[Serializable]
public class MapPiller
{
    public int identity = 0;
    public int monsterCount = 0;
    public int life = 0;
   
}
//Used by class ProceduralMapGrowth to save the map data.
[Serializable]
public class MapBlock
{
    public MapPiller[] pillars;
}