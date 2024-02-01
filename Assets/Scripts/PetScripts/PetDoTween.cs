using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PetDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(9, 1).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
