using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameOverPanel;
    //[SerializeField] private GameObject mainMenuPanel;

    private void Start()
    {
        GameManager.Instance.UIRegister(this);
        _scoreText.text = "Score : 000";
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score : "+score.ToString("000");
    }

    public void OpenGameOverPanel()
    {
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
