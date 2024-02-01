using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims;

public class Player : MonoBehaviour
{
    public static Player i;
    private RPGCharacterController rpgCharacterController;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float STR;
    public float DEX;
    public float INT;
    [Header("Bools")]
    public bool isDead;
    [Header("Variables")]
    public Animator anim;
    public Plane plane;
    public PhotonView view;
    public MMProgressBar healthBar;
    public TMP_Text healthText;
    [Header("Feedbacks")]
    
    [SerializeField] private MMF_Player mmfGetHit;
    [SerializeField] private MMF_Player mmfDeath;
    [SerializeField] private MMF_Player mmfDeathLocal;
    [SerializeField] private MMF_Player mmfRevive;
    [SerializeField] private MMF_Player mmfHeal;


    private void Awake()
    {
        rpgCharacterController = this.GetComponent<RPGCharacterController>();
        view = GetComponent<PhotonView>();
        if (view.IsMine)
            i = this;
    }
    private void Start()
    {
        if (view.IsMine)
            GameManager.i.playerList.Add(this);
            //Cursor.lockState = CursorLockMode.None;
            plane = new Plane(Vector3.up, Vector3.zero);

        anim = GetComponent<Animator>();
        //controller = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        if (!view.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.R))
            view.RPC("Revive", RpcTarget.All);
        if (Input.GetKeyDown(KeyCode.H))
            view.RPC("Heal", RpcTarget.All, 10f);
    }
    [PunRPC]
    public void ChangeStats(float _STR,float _DEX,float _INT)
    {
        STR += _STR;
        DEX += _DEX;
        INT += _INT;
    }
    [PunRPC]
    public void TakeDamage(float _damage)
    {
        if (isDead)
            return;
        mmfGetHit.PlayFeedbacks();
        //healthBar.Minus10Percent();
        currentHealth -= _damage;
        //healthText.text = currentHealth.ToString("0");
        if(currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    [PunRPC]
    public void Heal(float _heal)
    {
        if (isDead)
            return;
        mmfHeal.PlayFeedbacks();
        if(_heal + currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += _heal;
        }
    }

    public void Die()
    {
        isDead = true;
        rpgCharacterController.TryStartAction(HandlerTypes.Death);
        if (view.IsMine)
        {
            EUActions.OnDeath?.Invoke();
            mmfDeathLocal.PlayFeedbacks();


        }
        else
        {
            mmfDeath.PlayFeedbacks();
        }
        
       
    }

 
    [PunRPC]
    public void Revive()
    {
        EUActions.OnRevive?.Invoke();
        isDead = false;
        rpgCharacterController.TryEndAction(HandlerTypes.Death);
        mmfRevive.PlayFeedbacks();
        currentHealth = maxHealth;

    }

}
