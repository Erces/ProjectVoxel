using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponTrigger : MonoBehaviour
{
    public EUWeapon weapon;
    public bool disableOnImpact;
    public enum Type { STR,INT,DEX}
    public Type WeaponType;
    public BoxCollider col;

    private void Start()
    {

        col = GetComponent<BoxCollider>();

    }

    public void HitEnemies()
    {
        var gmj = Physics.OverlapSphere(transform.position, .7f);
        foreach (var item in gmj)
        {
            if (item.CompareTag("Enemy"))
            {
                var dmg = 0f;
                Debug.Log("Damage");
                switch (WeaponType)
                {
                    case Type.STR:
                        dmg = weapon.baseDamage * Mathf.Log(Player.i.STR, 2);
                        break;
                    case Type.DEX:
                        break;
                    case Type.INT:
                        break;
                    default:
                        break;
                }
                Debug.Log(EULog.exc+ "Given damage = " + dmg);
                PlayerFeedbacks.i.MeeleHit();
               
                var vfx = (GameObject)Instantiate(weapon.vfxOnHit, item.ClosestPoint(transform.position),Quaternion.identity);
                item.GetComponent<Enemy>().view.RPC("TakeDamage", RpcTarget.All, dmg);
                if (disableOnImpact)
                    col.enabled = false;
            }
        }
        
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position , 1);
    }


}
