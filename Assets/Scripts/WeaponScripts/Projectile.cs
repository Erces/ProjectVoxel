using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Projectile : MonoBehaviour
{
    PhotonView view;
    public float damage = 10;
    public GameObject hitVFX;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            other.GetComponent<Enemy>().view.RPC("TakeDamage", Photon.Pun.RpcTarget.All,damage);
            other.GetComponent<Enemy>().view.RPC("Pushback", Photon.Pun.RpcTarget.All, transform.position);
            view.RPC("ShootEffects",RpcTarget.All, other.GetComponent<Enemy>().view.ViewID); 
            var vfx = (GameObject)Instantiate(hitVFX,transform.position,transform.rotation);
            vfx.transform.SetParent(null);
            PhotonNetwork.Destroy(view);
        }
    }
    [PunRPC]
    private void ShootEffects(int id){
        var enemy = PhotonView.Find(id);
        enemy.GetComponent<Enemy>().foundPlayer = true;
    }
}
