using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.AI;


public class Room : MonoBehaviour
{
    public enum Type {Starter,Combat,Deck,Trap,Prize}
    public Type roomType;
    public GameObject starterTile;
    public GameObject portal;
    public int maxTileCount;
    public int tileCount = 0;
    public List<Tile> tiles;
    public List<NavMeshSurface> spawnedSurfaces;
    public List<Tile> spawnedTiles;

    private PhotonView view;
    [Header("Transforms")]
    [SerializeField] private Transform centerPoint;
    public Transform playerSpawnPoint;
    // 0: right
    // 1: left
    // 2: forward
    // 3: backward
    public List<Transform> roomSpawnPoint;
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private int enemyCount;
    [SerializeField] private Transform chestSpawnPos;
    private int counter = 0;
    private bool hasSpawned;
    void OnEnable()
    {
        EUActions.OnTileCleared += SpawnChest;
        EUActions.OnTileCleared += SpawnPortal;
        EUActions.OnTileSpawn += IncreaseTileCount;
        EUActions.OnAllTilesSpawn += BakeNavmesh;
        EUActions.OnMapBake += PlaceEnemies;
    }
    private void OnDisable()
    {
        EUActions.OnTileCleared -= SpawnChest;
        EUActions.OnTileCleared -= SpawnPortal;
        EUActions.OnTileSpawn -= IncreaseTileCount;
        EUActions.OnAllTilesSpawn -= BakeNavmesh;
        EUActions.OnMapBake -= PlaceEnemies;
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        hasSpawned = false;
        centerPoint = RoomManager.i.tileTransform;
    }
    public void Activate()
    {
        Debug.Log(EULog.excBlue + "Activating tile");
        if (PhotonNetwork.IsMasterClient)
        {
            tiles = ShuffleTiles(tiles);
            Debug.Log(EULog.exc + "Spawning Enemies");
            StartCoroutine("SpawnEnemies");
            EUActions.OnRoomActivate?.Invoke();
            starterTile = PhotonNetwork.Instantiate("Tiles/Starter",RoomManager.i.tileTransform.position,RoomManager.i.tileTransform.rotation);
            starterTile.GetComponent<Tile>().room = this;
            
        }
    }
    public void PlaceEnemies(){
        if(hasSpawned)
        return;
        Debug.Log(EULog.star + " All tiles spawned");
        hasSpawned = true;
        spawnedTiles.RemoveAt(0);
        spawnedTiles = ShuffleTiles(spawnedTiles);
        foreach (var tile in spawnedTiles)
        {
            var rndm = Random.Range(0,2);
            if(rndm == 0){
            var enemy = PhotonNetwork.Instantiate("Enemy_Goblin",tile.enemySpawnPoints[0].position,Quaternion.identity);
            var enemySec = PhotonNetwork.Instantiate("Enemy_Bat",tile.enemySpawnPoints[1].position,Quaternion.identity);
            var enemyThird = PhotonNetwork.Instantiate("Enemy_Slime",tile.enemySpawnPoints[2].position,Quaternion.identity);

            }
            else{
            var enemy = PhotonNetwork.Instantiate("Enemy_Bat",tile.enemySpawnPoints[0].position,Quaternion.identity);

            }

        }
        view.RPC("OnEnemiesPlaced",RpcTarget.All);

    }

    [PunRPC]
    public void OnEnemiesPlaced(){
        EUActions.OnEnemiesPlaced?.Invoke();
    }
    public void BakeNavmesh(){
        NavMesh.RemoveAllNavMeshData();
        foreach (var item in spawnedSurfaces)
        {
            item.BuildNavMesh();
        }
        Invoke("PlaceEnemies",6.5f);
    }

    private List<Tile> ShuffleTiles(List<Tile> _list){
        var shuffledTiles = _list.OrderBy(x => Random.value).ToList();
        return shuffledTiles;
    }

    private void IncreaseTileCount(){
        tileCount++;
    }
    // Update is called once per frame
    #region Spawn Things
    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(4);
            GameObject goblin =PhotonNetwork.Instantiate("Enemy_Goblin",  enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].position, Quaternion.identity);
            GameObject slime = PhotonNetwork.Instantiate("Enemy_Slime", enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].position, Quaternion.identity);
            //slime.transform.SetParent(TileManager.i.currentTile.transform);
        }

    }

    public void SpawnChest()
    {
        Debug.Log(EULog.star + "Spawning Chest");
        if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.Instantiate("Chest", RoomManager.i.tileTransform.position + Vector3.up * 7.65f, RoomManager.i.tileTransform.rotation);
    }
    public void SpawnPortal(){
        PhotonNetwork.Instantiate("Portal",starterTile.GetComponent<DropTile>().portalSpawnPos.position,starterTile.GetComponent<DropTile>().portalSpawnPos.rotation);
    }

    #endregion
}
