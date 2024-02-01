using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pet : MonoBehaviour
{
    public PhotonView ownerPlayer;
    [SerializeField] private string petName { get; set; }
    private int petLevel;
    public float STR;
    public float DEX;
    public float INT;

    public int petID;

    private void Start()
    {
        if(ownerPlayer.IsMine)
        Invoke("BoostPlayer", 1f);
    }

    private void BoostPlayer()
    {
        ownerPlayer.RPC("ChangeStats", RpcTarget.All, STR,DEX,INT);
    }
}
