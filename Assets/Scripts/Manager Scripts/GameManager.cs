using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public bool isGameOver = false;

    private UIScripts uiScripts;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore()
    {
        score += 10;
        uiScripts.UpdateScoreText(score);
    }

    public void UIRegister(UIScripts ui)
    {
        uiScripts = ui;
    }

    private void Update()
    {
        if (isGameOver==true)
        {
            uiScripts.OpenGameOverPanel();
        }
    }
}
