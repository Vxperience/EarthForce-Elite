using UnityEngine;

public class EFE_DestroyMap : MonoBehaviour
{
    void Update()
    {
        // Destroy the chunk map after desapear of the screen
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
}
