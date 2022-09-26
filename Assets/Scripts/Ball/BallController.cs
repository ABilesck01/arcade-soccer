using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 origin;
    private TrailRenderer trailRenderer;
    private MeshRenderer ballRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = transform.position;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        ballRenderer = GetComponentInChildren<MeshRenderer>();
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
        ballRenderer.gameObject.SetActive(false);
        trailRenderer.gameObject.SetActive(false);
        yield return new WaitForSeconds(GameManager.reset);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = origin;
        rb.isKinematic = false;
        trailRenderer.gameObject.SetActive(true);
        ballRenderer.gameObject.SetActive(true);
    }
}
