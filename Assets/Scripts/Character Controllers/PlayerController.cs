using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody playerRigidbody;
    Vector3 moveDirection;
    Transform cameraObject;
    AnimationControls animationControls;
    CameraManager cameraManager;

    // public float walkingSpeed = 1.5f;
    public float movementSpeed = 7;
    public float sprintingSpeed = 10;
    public float rotationSpeed = 15;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isGrounded;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        animationControls = GetComponent<AnimationControls>();
        cameraManager = FindObjectOfType<CameraManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animationControls.UpdateAnimatorValues();
    }

    private void FixedUpdate()
    {
        HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }

    private void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (animator.GetBool("IsInteracting"))
            return;
        
        HandleMovement();
        HandleRotation();
    }

    public void HandleMovement()
    {

        moveDirection = cameraObject.forward * Input.GetAxis("Vertical");
        moveDirection = moveDirection + cameraObject.right * Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            playerRigidbody.velocity = moveDirection * sprintingSpeed;
        } else
        {
            playerRigidbody.velocity = moveDirection * movementSpeed;
        }
        
    }

    public void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * Input.GetAxis("Vertical");
        targetDirection += cameraObject.right * Input.GetAxis("Horizontal");
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;

        if (!isGrounded)
        {
            if (!animator.GetBool("IsInteracting"))
            {
                animationControls.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !animator.GetBool("IsInteracting"))
            {
                animationControls.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
    }
}
