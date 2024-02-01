using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "TileSystem/Chapter", order = 1)]
public class Chapter : ScriptableObject
{
    public int roomCount;
    public int maxRoomWithEnemy;
    [Range(0,10)] public int maxTrapRoomCount;
    [Range(0,10)] public int maxPrizeRoomCount;
    [Range(0,10)] public int maxDeckRoomCount;

    //Combat room count = roomCount - (trapRoomCount + prizeRoomCount + DeckRoomCount)


    
}
