using UnityEngine;

public class SphereScript : MonoBehaviour
{
    Renderer sphereRenderer;
    Color originalColor;

    Animator playerAnimator;
    bool detected = false;

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        originalColor = sphereRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // DETECT THE PLAYER
        if (other.GetComponent<CharacterController>() != null)
        {
            playerAnimator = other.GetComponent<Animator>();
            detected = true;
            sphereRenderer.material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // NO DETECTION
        if (other.GetComponent<CharacterController>() != null)
        {
            detected = false;
            sphereRenderer.material.color = originalColor;
            playerAnimator = null;
        }
    }

    void Update()
    {
        if (detected && playerAnimator != null)
        {
            AnimatorStateInfo playerInfo =
                playerAnimator.GetCurrentAnimatorStateInfo(0);

            // SKILL 2
            if (playerInfo.IsName("Skill 2"))
            {
                Destroy(gameObject);
            }
        }
    }
}
