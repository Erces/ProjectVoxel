using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour
{
    public int tileID;
    private NavMeshSurface surface;
    public Room room;
    public List<Transform> enemySpawnPoints;
    void Start()
    {
        surface = this.GetComponent<NavMeshSurface>();
        room.spawnedSurfaces.Add(surface);
        room.spawnedTiles.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
