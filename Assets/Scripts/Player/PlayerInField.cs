using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInField : MonoBehaviour
{
    public bool IsSelected = false;

    [SerializeField] private GameObject ring;
    
    public Transform myTransform;

    [Header("Components")]
    public PlayerMovement playerMovement;
    public PlayerKick playerKick;
    [Header("AI components")]
    public NavMeshAgent navMeshAgent;

    private Vector3 origin;

    private void Start()
    {
        myTransform = transform;
        DisableControl();
        origin = transform.position;
    }

    private void OnEnable()
    {
        GoalController.onScoreGoal += GoalController_onScoreGoal;
    }

    private void OnDisable()
    {
        GoalController.onScoreGoal -= GoalController_onScoreGoal;
    }

    private void GoalController_onScoreGoal(object sender, GoalController.OnGoalEventArgs e)
    {
        StartCoroutine(ResetBall());
    }

    private IEnumerator ResetBall()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        yield return new WaitForSeconds(GameManager.reset);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = origin;
        rb.isKinematic = false;
    }

    public Vector3 GetPosition()
    {
        return myTransform.position;
    }

    [ContextMenu("Enable Control")]
    public void EnableControl()
    {
        SetControl(true);
    }

    [ContextMenu("Disable Control")]
    public void DisableControl()
    {
        SetControl(false);
    }

    public void SetControl(bool isOnControl)
    {
        IsSelected = isOnControl;
        ring.SetActive(isOnControl);
    }
}
