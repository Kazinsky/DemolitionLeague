using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour {

    [SerializeField]
    public Animator thisAnimator;

    private Text popUptext;


    void OnEnable()
    {
        //Get clip info in order to delete object after it's done
        AnimatorClipInfo[] clipInfo = thisAnimator.GetCurrentAnimatorClipInfo(0);
        popUptext = thisAnimator.GetComponent<Text>();

        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetText(string text)
    {
        popUptext.text = text;
    }

    public void SetFontSize(int num)
    {
        popUptext.fontSize = num;
    }

    public void SetColor(Color value)
    {
        popUptext.color = new Color(value.r, value.g, value.b, value.a);
    }
}
