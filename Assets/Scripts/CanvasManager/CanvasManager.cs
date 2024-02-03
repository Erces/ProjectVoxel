using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager i;
    public List<WeaponContainer> containers;
    public GameObject containerHolder;
    
    void Awake()
    {
        i = this;
    }
    void OnEnable()
    {
        EUActions.OnItemPickUp += ContainerDisable;
    }
    void OnDisable()
    {
        EUActions.OnItemPickUp -= ContainerDisable;

    }
    public void FillContainers(WeaponItem _item){
        Debug.Log(EULog.excBlue + EULog.star + _item.type);
        foreach (var item in containers)
        {
        item.ManageVariables(_item);
        }
    }
    public void ContainerEnable(){
        containerHolder.SetActive(true);
    }
    public void ContainerDisable(){
        containerHolder.SetActive(false);
    }
}
