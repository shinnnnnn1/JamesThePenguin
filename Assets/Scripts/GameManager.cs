using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(instance == null)
            {
                instancee = FindObjectOfType<GameManager>();
            }
            return instancee;
        }
    }
    private static GameManager instancee;

}
