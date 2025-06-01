using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterHealthBar : MonoBehaviour
{
    [SerializeField] IFighter fighter;
    [SerializeField] Slider HPBar;
    [SerializeField] TextMeshProUGUI HPText;


    void Start()
    {
        HPBar.maxValue = fighter.MaxHP;
        HPBar.value = fighter.CurrentHP;

        fighter.OnCurrentHPChange += UpdateDisplay;

        SetHPText();
        fighter.OnCurrentHPChange += SetHPText;
    }
 
    public void UpdateDisplay() {
        HPBar.value = fighter.CurrentHP;
    }

    public void SetHPText() {
        HPText.text = fighter.CurrentHP + " / " + fighter.MaxHP;
    }
}
