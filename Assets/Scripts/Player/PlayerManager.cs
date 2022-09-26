using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerInField> PlayersInFields = new List<PlayerInField>();

    public PlayerInputs PlayerInputs;

    private PlayerInField currentControledPlayer;
    private int currentPlayerIndex = 0;
    private bool hasChangedPlayer = false;
    private bool isHoldingKick = false;

    private bool lockControls = false;

    private void Awake()
    {
        if(PlayersInFields.Count == 0)
        {
            Debug.LogError("Player in field list is empty", this);
            return;
        }

        SetControledPlayer(0);
    }

    private void OnEnable()
    {
        GameManager.onFinishGame += GameManager_onFinishGame;
        GoalController.onScoreGoal += GoalController_onScoreGoal;
    }

    

    private void OnDisable()
    {
        GameManager.onFinishGame -= GameManager_onFinishGame;
        GoalController.onScoreGoal -= GoalController_onScoreGoal;
    }

    private void GameManager_onFinishGame(object sender, System.EventArgs e)
    {
        lockControls = true;
    }

    private void GoalController_onScoreGoal(object sender, GoalController.OnGoalEventArgs e)
    {
        StartCoroutine(TimerLockControls());
    }

    private IEnumerator TimerLockControls()
    {
        lockControls = true;
        yield return new WaitForSeconds(GameManager.reset);
        lockControls = false;
    }

    private void Update()
    {
        if (lockControls) return;

        HandleChangePlayer();
        HandleKickInputs();
        HandleDash();
    }

    private void FixedUpdate()
    {
        if (lockControls) return;

        HandleControlMovement();
    }

    private void SetControledPlayer(int index)
    {
        for (int i = 0; i < PlayersInFields.Count; i++)
        {
            if (i == index)
            {
                currentControledPlayer = PlayersInFields[i];
                PlayersInFields[i].IsSelected = true;
                PlayersInFields[i].SetControl(true);
            }
            else
            {
                PlayersInFields[i].IsSelected = false;
                PlayersInFields[i].SetControl(false);
            }
        }
    }

    private void HandleChangePlayer()
    {
        if (PlayerInputs.passBall && !hasChangedPlayer)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % PlayersInFields.Count;
            SetControledPlayer(currentPlayerIndex);
            hasChangedPlayer = true;
        }
        if (!PlayerInputs.passBall)
        {
            hasChangedPlayer = false;
        }
    }

    private void HandleKickInputs()
    {
        if(PlayerInputs.kickBall && !isHoldingKick)
        {
            currentControledPlayer.playerKick.StartKick();
            isHoldingKick = true;
        }
        else if(PlayerInputs.kickBall && isHoldingKick)
        {
            currentControledPlayer.playerKick.HandleKick();
        }
        else if (!PlayerInputs.kickBall && isHoldingKick)
        {
            currentControledPlayer.playerKick.KickBall();
            isHoldingKick = false;
        }
    }

    private void HandleControlMovement()
    {
        if (currentControledPlayer != null)
        {
            currentControledPlayer.playerMovement.HandleMovement(PlayerInputs.movementInput);
        }
    }

    private void HandleDash()
    {
        if(PlayerInputs.dash)
        {
            currentControledPlayer.playerMovement.HandleDash();
        }
    }
}
