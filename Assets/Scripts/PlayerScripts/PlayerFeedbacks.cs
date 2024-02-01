using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Photon.Pun;
public class PlayerFeedbacks : MonoBehaviour
{
    public static PlayerFeedbacks i;

    public MMF_Player deathFeedback = null;
    public MMF_Player getHitFeedback = null;
    public MMF_Player shootFeedback = null;
    public MMF_Player meeleHitFeedback = null;

    private PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if(!view.IsMine)
        return;
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Death()
    {
        deathFeedback.PlayFeedbacks();
    }
    public void GetHit()
    {
        getHitFeedback.PlayFeedbacks();
    }
    public void ProjectileShoot()
    {
        shootFeedback.PlayFeedbacks();
    }
    public void MeeleHit()
    {
        meeleHitFeedback.PlayFeedbacks();
    }
}
