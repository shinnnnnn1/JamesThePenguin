using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IAction
{
    public bool end;

    public void OnAction()
    {
        if(!end)
        {
            Animation anim = GetComponentInParent<Animation>();
            anim.Play();
            end = true;
        }
    }
}
