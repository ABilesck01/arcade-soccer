using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInteraction : MonoBehaviour
{
    public GameObject interaction;
    public LayerMask interactionLayer;
    [Space]
    [SerializeField] private PlayerMovement playerMovement;
    
    private Transform _transform;
    private Vector2 movementInput;
    private bool interactionFlag = false;
    private bool hasInteracted = false;

    private void Start()
    {
        _transform = transform;
    }

    public void GetMovementInputs(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void GetInteraction(InputAction.CallbackContext context)
    {
        interactionFlag = context.action.triggered;
    }

    private void FixedUpdate()
    {
        playerMovement.HandleMovement(movementInput);
    }

    private void Update()
    {
        CheckForInteraction();
        if(interaction != null && interactionFlag && !hasInteracted)
        {
            if (interaction.TryGetComponent(out InWorldButton interactionButton))
            {
                hasInteracted = true;
                interactionButton.InvokeInteraction();
            }
        }
        if(!interactionFlag && hasInteracted)
        {
            hasInteracted = false;
        }
    }

    private void CheckForInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, 1, interactionLayer);
        if (colliders.Length > 0)
        {
            interaction = colliders[0].gameObject;
        }
        else
        {
            interaction = null;
        }
    }
}
