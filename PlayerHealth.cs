using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int life = 10;

    public void TakeDamage()
    {
        if (life > 0)
        {
            life = life - 1;
        }
    }
}
