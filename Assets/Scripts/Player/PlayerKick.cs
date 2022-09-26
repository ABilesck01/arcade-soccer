using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private LayerMask KickLayer;
    [SerializeField] private float KickDistance = 1f;
    [Header("Kick settings")]
    [SerializeField] private float minKickForce = 0;
    [SerializeField] private float maxKickForce = 0;
    [SerializeField] private float chargeSpeed = 0;
    [Header("DEBUG")]
    [SerializeField] private bool ShowGizmos;

    private float currentKickForce;
    private bool hasKicked;

    public void StartKick()
    {
        currentKickForce = minKickForce;
        hasKicked = false;
    }

    public void HandleKick()
    {
        if (currentKickForce < maxKickForce)
        {
            currentKickForce += chargeSpeed * Time.deltaTime;
        }
        else
        {
            KickBall();
        }
    }

    public void KickBall()
    {
        hasKicked = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, KickDistance, KickLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(transform.forward * currentKickForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, KickDistance);
        }
    }
}
