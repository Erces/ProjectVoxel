using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class EUActions
{
    public static Action<Enemy> OnEnemyDeath;
    public static Action OnGameStart;
    public static Action OnTileCleared;
    public static Action OnItemPickUp;
    public static Action OnNextRoom;
    public static Action OnNextTileSpawned;
    public static Action OnDeath;
    public static Action OnRevive;
    
    public static Action OnRoomActivate;
    public static Action OnTileSpawn;
    public static Action OnAllTilesSpawn;
    public static Action OnMapBake;
    public static Action OnEnemiesPlaced;
}
