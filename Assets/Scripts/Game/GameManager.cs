using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float MatchTimeInSeconds = 90f;
    [SerializeField] private float timeToReset = 2f;
    [Space]
    [SerializeField] private int teamOneScore = 0;
    [SerializeField] private int teamTwoScore = 0;
    [Space]
    [SerializeField] private GameUI gameUI;

    private float currentTimeInSeconds;
    private bool pauseTimer;

    public static float reset;

    public static event EventHandler onFinishGame;
    public static event EventHandler onResetGame;

    private void Start()
    {
        reset = timeToReset;
        //pauseTimer = true;
    }

    private void OnEnable()
    {
        GoalController.onScoreGoal += GoalController_onScoreGoal;
        GameManager.onFinishGame += GameManager_onFinishGame;
    }

    private void OnDisable()
    {
        GoalController.onScoreGoal -= GoalController_onScoreGoal;
        GameManager.onFinishGame -= GameManager_onFinishGame;
    }

    private void Update()
    {
        if(!pauseTimer)
        {
            currentTimeInSeconds += Time.deltaTime;
            float minutes = Mathf.FloorToInt(currentTimeInSeconds / 60);
            float seconds = Mathf.FloorToInt(currentTimeInSeconds % 60);
            gameUI.SetTextTxtTimer($"{minutes.ToString("00")}:{seconds.ToString("00")}");
        }

        if(currentTimeInSeconds >= MatchTimeInSeconds)
        {
            onFinishGame?.Invoke(this, null);

            int team = 0;
            if (teamOneScore > teamTwoScore)
                team = 1;
            else if(teamTwoScore > teamOneScore)
                team = 2;

            gameUI.OpenFinishModal(team);
        }

    }

    private void GoalController_onScoreGoal(object sender, GoalController.OnGoalEventArgs e)
    {
        StartCoroutine(StartPauseTimer());
        if(e.team == Team.team1)
        {
            teamOneScore++;
            gameUI.SetTextTxtScoreOne(teamOneScore.ToString());
        }
        else if(e.team == Team.team2)
        {
            teamTwoScore++;
            gameUI.SetTextTxtScoreTwo(teamTwoScore.ToString());
        }
    }

    private void GameManager_onFinishGame(object sender, EventArgs e)
    {
        pauseTimer = true;
    }

    private IEnumerator StartPauseTimer()
    {
        pauseTimer = true;
        yield return new WaitForSeconds(reset);
        onResetGame?.Invoke(this, null);
        pauseTimer = false;
    }

}
