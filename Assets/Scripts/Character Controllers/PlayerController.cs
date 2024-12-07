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
    public bool isJumping;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        animationControls = GetComponent<AnimationControls>();
        cameraManager = FindObjectOfType<CameraManager>();
        animator = GetComponent<Animator>();
        isGrounded = true;
    }

    private void Update()
    {
        animationControls.UpdateAnimatorValues();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            HandleJumping();
        }
    }

    private void FixedUpdate()
    {
        HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
        animator.SetBool("isGrounded", isGrounded);
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
        Vector3 targetPosition;
        rayCastOrigin.y += rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded)
        {
            if (!animator.GetBool("IsInteracting"))
            {
                animationControls.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);

            if (inAirTimer >= 5)
            {
                playerRigidbody.AddForce(Vector3.up * 15, ForceMode.Impulse);
            }
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, rayCastHeightOffset + 0.1f, groundLayer))
        {
            if (!isGrounded && !animator.GetBool("IsInteracting"))
            {
                animationControls.PlayTargetAnimation("Land", true);
            }

            if (!isJumping)
            {
                Vector3 rayCastHitPoint = hit.point;
                targetPosition.y = rayCastHitPoint.y;
                inAirTimer = 0;
                isGrounded = true;
                isJumping = false;
            }
        } else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (animator.GetBool("IsInteracting") || movementSpeed > 0)
            {
                playerRigidbody.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f));
            }
            else
            {
                transform.position = targetPosition;
            }
        }

        if (isJumping && playerRigidbody.velocity.y <= 0)
        {
            isJumping = false;
        }
    }

    private void HandleJumping()
    {
        if (isGrounded)
        {
            isGrounded = false;
            animationControls.animator.SetBool("isJumping", true);
            animationControls.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);

            Vector3 playerVelocity = moveDirection * movementSpeed;
            playerVelocity.y = jumpingVelocity;

            playerRigidbody.velocity = playerVelocity;
        }
    }
}
