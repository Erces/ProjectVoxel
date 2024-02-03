using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PickUp : MonoBehaviour
{

    public ChestItems chest;
    public Transform dropPos;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                var gmj = (GameObject)Instantiate(chest.legendaryPet, transform.position, transform.rotation);
                gmj.transform.DOJump(dropPos.position, 3, 2, 1.5f);
            }
        }
    }
}
