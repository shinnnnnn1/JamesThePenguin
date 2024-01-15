using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    public int point;
    public int currentPoint;

    public bool dead;
    public GameObject head;
    public GameObject rag;

    public string currnetScene;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Gameover()
    {
        head.SetActive(false);
        rag.SetActive(true);
        UIManager.instance.ammoText.gameObject.SetActive(false);
        UIManager.instance.slider.gameObject.SetActive(false);
        UIManager.instance.sight.gameObject.SetActive(false);
        UIManager.instance.gameover.gameObject.SetActive(true);
        UIManager.instance.gameover.DOFade(0.2f, 1f).OnComplete(Menu);
    }
    public void Menu()
    {
        UIManager.instance.menu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        SceneManager.LoadScene(currnetScene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
