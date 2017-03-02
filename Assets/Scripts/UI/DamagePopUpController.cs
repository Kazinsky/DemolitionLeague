using UnityEngine;
using System.Collections;

public class DamagePopUpController : MonoBehaviour {

    private static PopUpText popUpText;
    private static GameObject canvas;
    private static float minRandomValue = -.5f, maxRandomValue = .5f;
    private static int critFontsize = 30;
    private static int effectsFontsize = 15;
    private static Color normalColor = new Color(255, 0, 0, 255);
    private static Color fireColor = new Color(255, 69, 0, 255);
    private static Color bleedColor = new Color(255, 0, 0, 255);

    public static void Initialize()
    {
        canvas = GameObject.FindGameObjectWithTag("UIPopUp");

        if(!popUpText)
            popUpText = Resources.Load<PopUpText>("Prefabs/UI/DamageTextPopUp");

    }

    public static void CreateDamagePopUp(string text, Vector3 location, DamageType damageType)
    {
      
        PopUpText instance = Instantiate(popUpText);
       
        instance.transform.SetParent(canvas.transform, false);

        Vector3 position = new Vector3(location.x + Random.Range(minRandomValue, maxRandomValue), location.y + Random.Range(minRandomValue, maxRandomValue), location.z + Random.Range(minRandomValue, maxRandomValue));

        instance.transform.position = location;
        instance.SetText(text);

        if (damageType == DamageType.Normal)
        {
            instance.SetFontSize(effectsFontsize);
            instance.SetColor(normalColor);
        }

        else if (damageType == DamageType.Fire)
        {
            instance.SetFontSize(effectsFontsize);
            instance.SetColor(fireColor);
        }

     
    }

}
