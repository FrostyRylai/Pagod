using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    Animator playerAnimator;
    CharacterController playerController;

    public static bool checkAttack;

    // IF PLAYER NOT ON THE GROUND
    bool wasGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();

        checkAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        float ForwardBackward = Keyboard.current.wKey.ReadValue() > 0 ? 1 : Keyboard.current.sKey.ReadValue() > 0 ? -1 : 0;
        float LeftRight = Keyboard.current.dKey.ReadValue() > 0 ? 1 : Keyboard.current.aKey.ReadValue() > 0 ? -1 : 0;

        playerAnimator.SetFloat("LR", LeftRight);
        playerAnimator.SetFloat("FB", ForwardBackward);

        bool sprint = false;
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            sprint = true;
        }

        playerAnimator.SetBool("Sprint", sprint);

        // JUMP ANIMATION
        bool isGrounded = playerController.isGrounded;
        bool isFalling = false;

        if (wasGrounded == true &&
        isGrounded == false &&
        playerController.velocity.y > 0f)
        {
            playerAnimator.SetTrigger("Jump");
        }

        // DROP/FALL ANIMATION
        if (wasGrounded == true &&
            isGrounded == false &&
            playerController.velocity.y <= 0f)
        {
            playerAnimator.SetTrigger("Drop");
        }

        if (isGrounded == false && playerController.velocity.y < 0f)
        {
            isFalling = true;
        }

        playerAnimator.SetBool("IsGrounded", isGrounded);
        playerAnimator.SetBool("IsFalling", isFalling);

        wasGrounded = isGrounded;

        // ATTACK ANIMATION
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            playerAnimator.SetTrigger("Attack");
        }

        // SKILL 1 ANIMATION
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            playerAnimator.SetTrigger("Skill 1");
        }

        // SKILL 2 ANIMATION
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            playerAnimator.SetTrigger("Skill 2");
        }

        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            playerAnimator.SetTrigger("Attack");
        }

        // CHECK IF THE ATTACK ANIMATION IS PLAYING
        AnimatorStateInfo playerInfo =
            playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (playerInfo.IsName("Attack"))
        {
            if (checkAttack == false)
            {
                Debug.Log("Chest test: attack is active");
            }

            checkAttack = true;
        }
        else
        {
            checkAttack = false;
        }
    }
}
