using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(instancee == null)
            {
                instancee = FindObjectOfType<GameManager>();
            }
            return instancee;
        }
    }
    private static GameManager instancee;

    public enum State
    {
        Default, Menu, GameOver
    }
    public State state;

    void Update()
    {
        
    }
}
