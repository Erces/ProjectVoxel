using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims;
using Photon.Pun;
public class WeaponManager : MonoBehaviour
{
    private RPGCharacterController characterController;
    public static WeaponManager i;
    public WeaponItem leftHandWeapon;
    public WeaponItem rightHandWeapon;

    public enum WeaponType {MEELE,BOW,STAFF}
    public WeaponType currentWeaponType;
    public RPGCharacterController rpgCharacterController;

    private PhotonView view;
    public Transform arrowSpawnPos;
    public float arrowForce;
    [Header("TestArea")]
    public WeaponItem leftDagger;
    public WeaponItem rightDagger;
    public WeaponItem bow;
    public WeaponItem twoHandSword;
        public WeaponItem twoHandBow;

    

        void Start()
        {
            view = GetComponent<PhotonView>();
            if(!view.IsMine)
            return;
            characterController = this.GetComponent<RPGCharacterController>();
            i = this;
        }
    public void EquipWeapon(WeaponItem _weapon, bool leftHand){
        
        Debug.Log(EULog.excBlue + "Equip Weapon");
        var context = new SwitchWeaponContext();
        if(_weapon.isTwoHanded){
        context.type = "Switch";
		context.sheathLocation = "Back";
		context.rightWeapon = _weapon.weaponID;
		rpgCharacterController.TryStartAction(HandlerTypes.SwitchWeapon, context);
        rightHandWeapon = _weapon;
        }
        else{
            if(leftHand){
        context.type = "Switch";
		context.side = "Left";
		context.sheathLocation = "Back";
		context.leftWeapon = _weapon.weaponID;
		rpgCharacterController.TryStartAction(HandlerTypes.SwitchWeapon, context); 
        leftHandWeapon = _weapon;
            }
            else{
        context.type = "Switch";
		context.side = "Right";
		context.sheathLocation = "Back";
		context.rightWeapon = _weapon.weaponID;
		rpgCharacterController.TryStartAction(HandlerTypes.SwitchWeapon, context); 
        rightHandWeapon = _weapon;
            }
        }
        

		
        
		

    }

    public void WeaponAttack(Side side){
        if(rpgCharacterController.rightWeapon == Weapon.RightDagger && rpgCharacterController.leftWeapon == Weapon.LeftDagger){
            DualAttack();
        }
        else if(rpgCharacterController.rightWeapon.Is2HandedWeapon() || rpgCharacterController.leftWeapon.Is2HandedWeapon()){
            TwoHandedAttack();
        }
        else{
        rpgCharacterController.StartAction(HandlerTypes.Attack, new AttackContext(HandlerTypes.Attack, side, -1));
        }
    }
    public void RightWeaponAttack(){

    }
    public void TwoHandedAttack(){
        rpgCharacterController.StartAction(HandlerTypes.Attack, new AttackContext(HandlerTypes.Attack, Side.None, -1));
        
    }
    public void DualAttack(){
        rpgCharacterController.StartAction(HandlerTypes.Attack, new AttackContext(HandlerTypes.Attack, Side.Dual, -1));

    }
    public void ShootArrow()
    {
        var go = PhotonNetwork.Instantiate("Arrow", arrowSpawnPos.position, Quaternion.identity);
        PlayerFeedbacks.i.shootFeedback.PlayFeedbacks();
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
