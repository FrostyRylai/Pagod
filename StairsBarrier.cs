using UnityEngine;

public class StairsBarrier : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Sphere2;

    bool spheresAssigned = false;

    void Start()
    {
        if (Sphere1 != null && Sphere2 != null)
        {
            spheresAssigned = true;
        }
    }

    void Update()
    {
        if (spheresAssigned &&
            Sphere1 == null &&
            Sphere2 == null)
        {
            Destroy(gameObject);
        }
    }
}
