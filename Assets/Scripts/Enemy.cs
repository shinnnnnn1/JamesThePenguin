using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private void Start()
    {
        gameObject.Ragdoll(false);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.Ragdoll(true);
        }

    }
}
