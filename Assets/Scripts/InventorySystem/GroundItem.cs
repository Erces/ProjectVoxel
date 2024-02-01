using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GroundItem : MonoBehaviour
{
    //Ground Item Base Class for pets/weapons
    public Item inventoryItem;

    [SerializeField] private float pickupRange;
    private float distanceToPlayer;

    private bool canvasEnabled;
    private void Start()
    {  
        canvasEnabled = false;
        InvokeRepeating("CheckForRange", .3f, .3f);
        EUActions.OnItemPickUp += EaseToPlayer;
    }
    private void OnDisable()
    {
        EUActions.OnItemPickUp -= EaseToPlayer;

    }
    public void Update()
    {
        if (distanceToPlayer < pickupRange)
        {
            if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log(EULog.star + " Item picked up!");
            if(inventoryItem.isWeapon){
            Inventory.i.AddWeapon(inventoryItem);
                
            }
            else{
            Inventory.i.AddItem(inventoryItem);

            }
            EUActions.OnItemPickUp?.Invoke();
            }
        }
    }

    public void CheckForRange()
    {
        distanceToPlayer = Mathf.Abs(Vector3.Distance(Player.i.transform.position, this.transform.position));
    }
    
    void EaseToPlayer()
    {
        transform.DOScale(new Vector3(0, 0, 0), 1f);
        transform.DOJump(Player.i.transform.position + Vector3.up, 1, 1, .75f).OnComplete(() =>
        Destroy(gameObject)

        ); ; 
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            CanvasManager.i.ContainerDisable();
        }
    }
    void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("Player")){
            CanvasManager.i.ContainerEnable();
            CanvasManager.i.FillContainers((WeaponItem)inventoryItem);

        }
    }
}
