using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.VFX;

public class Skeleton : Enemy
{
    [SerializeField] private float damage;
    public bool canUseStone;
    public GameObject vfx;
    // Update is called once per frame
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            if(canUseStone){
                canUseStone = false;
                var stone = (GameObject)Instantiate(vfx,transform.position,transform.rotation);
                stone.transform.SetParent(null);
            }
            other.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,damage);
            Attack();
        }
    }

    private void Attack(){
        this.enemySituation = Enemy.Situation.Stop;
        stopTimer = 1f;
            anim.SetTrigger("Attack0");
            attackTimer = attackTime;
            canAttack = false;
            
    }
}
