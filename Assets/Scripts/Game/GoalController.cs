using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private Team teamToScore;
    [SerializeField] private float explosionDistance;
    [SerializeField] private float explosionForce;
    [SerializeField] private LayerMask explosionLayer;
    [Header("Optional")]
    [SerializeField] private GameObject explosionEffects;
    [Header("Debug only")]
    [SerializeField] private bool showGizmos;

    public static event EventHandler<OnGoalEventArgs> onScoreGoal;
    public class OnGoalEventArgs
    {
        public Team team;
    }

    private void OnEnable()
    {
        GameManager.onResetGame += GameManager_onResetGame;
    }

    private void OnDisable()
    {
        GameManager.onResetGame -= GameManager_onResetGame;
    }

    private void GameManager_onResetGame(object sender, EventArgs e)
    {
        explosionEffects.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            onScoreGoal?.Invoke(this, new OnGoalEventArgs()
            {
                team = teamToScore
            });
            explosionEffects.SetActive(true);

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionDistance, explosionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out Rigidbody rb))
                {
                    Debug.Log("KABOOM");
                    rb.AddExplosionForce(explosionForce, transform.position, explosionDistance, 1f, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.yellow;   
            Gizmos.DrawWireSphere(transform.position, explosionDistance);
        }
    }
}

public enum Team
{
    team1,
    team2
}
