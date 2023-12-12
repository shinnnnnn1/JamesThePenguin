using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (instancee == null)
            {
                instancee = FindObjectOfType<UIManager>();
            }
            return instancee;
        }
    }
    private static UIManager instancee;

    public TMP_Text ammoText;

    public void Ammo(int ammo, int fullAmmo)
    {
        ammoText.text = ammo.ToString() + " / " + fullAmmo;
    }
}
