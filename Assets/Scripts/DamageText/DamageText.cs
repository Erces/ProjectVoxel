using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using MoreMountains;
using MoreMountains.Feedbacks;
public class DamageText : MonoBehaviour {
    public Animator animator;
    public TMP_Text damageText;
    public MMF_Player mmfFeedbacks;

    void OnEnable()
    {
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
       // Debug.Log(clipInfo.Length);
        Destroy(gameObject, 1);
    }

    public void SetText(string text)
    {
        damageText.text = text;
        mmfFeedbacks.PlayFeedbacks();
    }
}