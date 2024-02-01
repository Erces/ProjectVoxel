using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public List<Player> playerList;

    private void Awake()
    {
        DOTween.Init();
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
