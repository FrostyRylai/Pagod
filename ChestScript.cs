using UnityEngine;

public class ChestScript : MonoBehaviour
{
    Animator chestAnimator;

    public ChestScript Chest1;
    public ChestScript Chest2;
    public ChestScript Chest3;
    public GameObject barrier2;

    public int hitCount = 0;
    bool countedThisAttack = false;

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
        barrier2 = GameObject.Find("barrier2");
    }

    void Update()
    {
        // COUNT NEXT ATTACK
        if (PlayerAnimation.checkAttack == false)
        {
            countedThisAttack = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Check what touched the chest
        if (other.name == "sword:sword" &&
            PlayerAnimation.checkAttack &&
            countedThisAttack == false &&
            hitCount < 3)
        {
            countedThisAttack = true;
            hitCount++;

            if (hitCount == 3)
            {
                chestAnimator.SetTrigger("Open");

                if (Chest1 != null && Chest2 != null && Chest3 != null &&
                    Chest1.hitCount >= 3 &&
                    Chest2.hitCount >= 3 &&
                    Chest3.hitCount >= 3)
                {
                    Destroy(barrier2);
                }
            }
        }
    }
}
