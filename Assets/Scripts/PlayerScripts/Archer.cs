using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;
using MoreMountains.Feedbacks;
public class Archer : Player
{
    public MMF_Player mmf;
    public MMF_Player mmfRMB;
    public GameObject arrow;
    public Transform arrowSpawnPos;
    public float arrowForce;



    public bool canLMB;
    public bool canRMB;
    public float lmbTimer;
    public float lmbTime;
    public float rmbTimer;
    public float rmbTime;
    public WeaponTrigger trigger = null;
    public float basicAttackAnimCount;

    private float zCam;
    private Quaternion targetRotation;
    public SphereCollider legHitCol;
   

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine)
            return;
        HandleTick();
        if (Input.GetKeyDown(KeyCode.Mouse0) && canLMB)
        {
            BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canRMB)
        {
            SpecialAttack();
        }
        //CameraFacing();
    }
    void HandleTick()
    {
        lmbTimer -= Time.deltaTime;
        rmbTimer -= Time.deltaTime;

        if (lmbTimer <= 0)
        {
            lmbTimer = 0;
            canLMB = true;
        }
        if (rmbTimer <= 0)
        {
            rmbTimer = 0;
            canRMB = true;
        }
    }
    void BasicAttack()
    {
        lmbTimer = lmbTime;
        var rndm = Random.Range(0, basicAttackAnimCount);
        anim.SetTrigger("Attack" + rndm.ToString("0"));
        Debug.Log("Attack" + rndm.ToString("0"));
        canLMB = false;

    }

    void SpecialAttack()
    {
        rmbTimer = rmbTime;
        anim.SetTrigger("AttackSpecial");
        canRMB = false;
    }
    public void CameraFacing()
    {
        Debug.Log("Camera Facing");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        if (plane.Raycast(ray, out var enter))
        {
            var hitPoint = ray.GetPoint(enter);
            var playerPositionOnPlane = plane.ClosestPointOnPlane(this.transform.position);
            this.transform.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }
    }
    public void ShootArrow()
    {
        var go = PhotonNetwork.Instantiate(arrow.name, arrowSpawnPos.position, Quaternion.identity);
        //var go = (GameObject)Instantiate(arrow, arrowSpawnPos.position, Quaternion.identity);
        view.RPC("SyncArrow", RpcTarget.All,go.GetComponent<PhotonView>().ViewID);
        //go.GetComponent<Rigidbody>().AddForce(this.transform.forward * arrowForce,ForceMode.Acceleration);
    }
    [PunRPC]
    public void SyncArrow(int id) 
    {
        PhotonView view = PhotonView.Find(id);
        view.transform.rotation = Quaternion.Euler(0, view.transform.rotation.y, 0);
        view.transform.rotation = Quaternion.Euler(0, view.transform.rotation.y, 0);
        view.transform.forward = this.transform.forward;

        view.GetComponent<Rigidbody>().velocity = this.transform.forward * arrowForce;
    }
}
