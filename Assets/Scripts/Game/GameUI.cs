using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI txtScoreOne;
    public TextMeshProUGUI txtScoreTwo;
    public TextMeshProUGUI txtTimer;
    [Space]
    [SerializeField] private GameObject FinishUI;
    public TextMeshProUGUI txtWinner;

    public void SetTextTxtScoreOne(string value)
    {
        txtScoreOne.text = value;
    }

    public void SetTextTxtScoreTwo(string value)
    {
        txtScoreTwo.text = value;
    }

    public void SetTextTxtTimer(string value)
    {
        txtTimer.text = value;
    }

    public void OpenFinishModal(int winnerTeam)
    {
        FinishUI.SetActive(true);
        if (winnerTeam == 1)
            txtWinner.text = "Orange team wins!";
        else if (winnerTeam == 2)
            txtWinner.text = "Purple team wins!";
        else
            txtWinner.text = "Draw!";
    }

    public void btnReset_Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
