using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController playerController;
    Animator playerAnimator;

    //Player Health
    PlayerHealth playerHealth;

    Vector3 respawnPosition;

    //Respawn
    bool needsRespawn = false;

    // Jump
    float jumpMovement = 0f;
    bool jumpHeld = false;

    // Knockback
    public static bool applyKnockback = false;
    float knockbackTimer = 0f;

    public float knockbackDuration = 1f;
    public float knockbackSpeed = 1f;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        respawnPosition = transform.position;
        applyKnockback = false;
    }

    void Update()
    {
        float moveSpeed = 0.010f;
        float sprintSpeed = 0.025f;

        // MOUSE MOVEMENT
        float mouseX = Mouse.current.delta.ReadValue().x;
        transform.Rotate(0f, mouseX * 0.3f, 0f);

        // SPRINTING
        if (Keyboard.current.leftShiftKey.isPressed &&
            Keyboard.current.wKey.ReadValue() > 0)
        {
            moveSpeed = sprintSpeed;
        }

        float ForwardBackward = Keyboard.current.wKey.ReadValue() > 0 ? moveSpeed : Keyboard.current.sKey.ReadValue() > 0 ? -moveSpeed : 0;
        float LeftRight = Keyboard.current.dKey.ReadValue() > 0 ? moveSpeed : Keyboard.current.aKey.ReadValue() > 0 ? -moveSpeed : 0;

        // JUMP
        float Jump = Keyboard.current.spaceKey.ReadValue() > 0 ? 0.15f : 0f;

        if (playerController.isGrounded)
        {
            jumpMovement = -0.0015f;

            if (Jump > 0 && jumpHeld == false)
            {
                jumpMovement = Jump;
            }
        }
        else
        {
            // Character is falling
            if (jumpMovement < 0f)
            {
                jumpMovement = jumpMovement - 0.0001f;
            }
            // Character is moving upward
            else
            {
                jumpMovement = jumpMovement - 0.0035f;
            }
        }

        jumpHeld = Jump > 0;

        // MOVEMENT
        Vector3 moveDirection = transform.TransformDirection(
            new Vector3(LeftRight, 0f, ForwardBackward)
        );

        // KNOCKBACK FROM BARRIER
        if (applyKnockback)
        {
            playerAnimator.SetTrigger("Knockback");
            knockbackTimer = knockbackDuration;
            applyKnockback = false;
        }

        if (knockbackTimer > 0f)
        {
            // Move backward while the animation plays
            Vector3 knockbackMovement =
                -transform.forward * knockbackSpeed * Time.deltaTime;

            playerController.Move(
                new Vector3(
                    knockbackMovement.x,
                    jumpMovement,
                    knockbackMovement.z
                )
            );

            playerController.Move(
                new Vector3(0f, jumpMovement, 0f)
            );

            knockbackTimer = knockbackTimer - Time.deltaTime;
        }
        else
        {
            playerController.Move(
                new Vector3(moveDirection.x, jumpMovement, moveDirection.z)
            );

            //MOUSE MOVEMENT
            playerController.Move(
                new Vector3(moveDirection.x, jumpMovement, moveDirection.z)
            );
        }

        // RESPAWN
        if (needsRespawn)
        {
            playerController.enabled = false;
            transform.position = respawnPosition;
            playerController.enabled = true;

            jumpMovement = 0f;
            knockbackTimer = 0f;
            applyKnockback = false;
            needsRespawn = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Safety Net" &&
            needsRespawn == false)
        {
            needsRespawn = true;
            playerHealth.TakeDamage();
        }

        if (hit.gameObject.name == "barrier" &&
            knockbackTimer <= 0f &&
            applyKnockback == false)
        {
            playerHealth.TakeDamage();
            applyKnockback = true;
        }
    }
}
