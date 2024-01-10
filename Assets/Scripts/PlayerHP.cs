using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics.Tracing;
using UnityEngine.UI;

public class PlayerHP : Character
{
    AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] Transform hit;
    [SerializeField] Slider slider;

    [SerializeField] Collider coll;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        slider.value = hp;
    }
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if(hp <= 0 && !dead)
        {
            dead = true;
            GameManager.instance.dead = true;
            GameManager.instance.Gameover();
            this.gameObject.tag = "Untagged";
        }
        source.PlayOneShot(clip);
        hit.DOShakeRotation(0.1f, 5, 1, 1, true);
        UIManager.instance.Hit();
    }
}
