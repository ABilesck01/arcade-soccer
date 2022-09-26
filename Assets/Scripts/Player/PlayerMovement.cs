using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float RotationForwardSpeed;
    public float DashForce;
    public float DashCooldown = 2f;

    //private CharacterController characterController;
    private Rigidbody rb;
    private Transform _transform;
    private Transform cam;

    private float turnSmoothVelocity;
    private float currentDashTime = 0;
    private bool canDash = true;
    private bool isDashing = false;

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        _transform = transform;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if(currentDashTime <= 0)
        {
            canDash = true;
        }
        else
        {
            currentDashTime -= Time.deltaTime;
        }
    }

    public void HandleMovement(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, RotationSpeed);
            _transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            //characterController.Move(moveDir.normalized * MovementSpeed * delta);
            //if(!isDashing)
            rb.velocity = moveDir.normalized * MovementSpeed;
        }
    }

    public void HandleForwardMovement()
    {
        rb.velocity = _transform.forward * MovementSpeed;
    }

    public void HandleForwardLook(Vector3 target)
    {
        Vector3 look = target - _transform.position;
        look.y = 0;
        Quaternion rot = Quaternion.LookRotation(look);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, rot, Time.deltaTime * RotationForwardSpeed);
    }

    public void HandleDash()
    {
        //StartCoroutine(Dash());
        //canDash = false;
        //currentDashTime = DashCooldown;
        //rb.AddForce(_transform.forward * DashForce, ForceMode.Impulse);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
    }
}
