using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [SerializeField] Image option;
    [SerializeField] Image fade;
    [SerializeField] string sceneName;

    public void GameStart() => FadeOut(true);
    public void GameOption() => option.gameObject.SetActive(!option.gameObject.activeSelf);
    public void GameExit() => FadeOut(false);

    void FadeOut(bool start)
    {
        string name = start ? "NextScene" : "Exit";
        fade.DOFade(1, 1).OnComplete(() => Invoke(name, 1f));
    }
    void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    void Exit()
    {
        Application.Quit();
    }
}
