using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerManager : MonoBehaviour
{
    public List<PlayerInField> PlayersInFields = new List<PlayerInField>();

    [Space]
    public Transform ball;

    private PlayerInField currentControledPlayer;
    private int currentPlayerIndex = 0;

    private void Start()
    {
        SetControledPlayer(0);
    }

    private void Update()
    {
        GetNearestPlayerFromBall();
        SetControledPlayer(currentPlayerIndex);

        currentControledPlayer.playerMovement.HandleForwardMovement();
        currentControledPlayer.playerMovement.HandleForwardLook(ball.position);
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

    private void GetNearestPlayerFromBall()
    {
        float distance = Mathf.Infinity;

        int index = 0;

        for (int i = 0; i < PlayersInFields.Count; i++)
        {
            float currentDistance = Vector3.Distance(PlayersInFields[i].transform.position, ball.position);
            if (distance > currentDistance)
            {
                distance = currentDistance;
                index = i;
            }
        }
        currentPlayerIndex = index;
    }
}
