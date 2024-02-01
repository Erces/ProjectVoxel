using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Chest : MonoBehaviour
{

    public List<Item> dropItems;
    public GroundItem dropItem;
    public Transform dropTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }
    void Drop()
    {
            var gmj = (GameObject)Instantiate(dropItem.gameObject, transform.position, transform.rotation);
            gmj.GetComponent<GroundItem>().inventoryItem = dropItems[Random.Range(0,dropItems.Count)];
            gmj.transform.DOJump(dropTransform.position, 3, 2, 1.5f);
    }
}
