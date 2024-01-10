using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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
    public Slider slider;
    public GameObject sight;
    public Image fade;
    public Color def;


    public Image gameover;
    public GameObject menu;

    public void Start()
    {
        def = fade.color;
    }
    public void Ammo(int ammo, int fullAmmo)
    {
        ammoText.text = ammo.ToString() + " / " + fullAmmo;
    }

    public void Hit()
    {
        Debug.Log("asdasd");
        fade.color = def;
        fade.gameObject.SetActive(true);
        fade.DOFade(0, 0.5f);
    }
}
