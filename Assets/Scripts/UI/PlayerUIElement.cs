using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIElement : MonoBehaviour {

    [SerializeField]
    private StatBar healthBar;

    [SerializeField]
    private Image playerIcon;

    [SerializeField]
    private Image playerColor;

    [SerializeField]
    private Image abilityIcon;

    [SerializeField]
    private Image weaponIcon;

    [SerializeField]
    private Text ammoCount;

    //Setters
    public void ChangeCurrentHealthValue(float health)
    {
        healthBar.SetCurrentValue(health);
    }

    public void ChangeMaxHealthValue(float health)
    {
        healthBar.SetMaxValue(health);
    }

    public void ChangePlayerIcon(Sprite sprite)
    {
            playerIcon.sprite = sprite;
    }

    public void ChangePlayerColor(Sprite sprite)
    {
            playerColor.sprite = sprite;
    }

    public void ChangeAbilityIcon(Sprite sprite)
    {
            abilityIcon.sprite = sprite;
    }


    public void ChangeWeaponIcon(Sprite sprite)
    {
            weaponIcon.sprite = sprite;
    }

    public void ChangeAmmoCount(string text)
    {
            ammoCount.text = text;
    }

}
