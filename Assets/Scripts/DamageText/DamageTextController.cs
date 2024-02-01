using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;
using Cinemachine;
public class DamageTextController : MonoBehaviour {
    public static DamageText popupText;
    private static GameObject canvas;
    public static CinemachineVirtualCamera cam;
void Start()
{
    
    Initialize();
}
    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        
        if (!popupText)
            popupText = Resources.Load<DamageText>("PopUpTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        DamageText instance = Instantiate(popupText);
    
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x , location.position.y ));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}