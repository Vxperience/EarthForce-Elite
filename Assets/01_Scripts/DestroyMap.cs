using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMap : MonoBehaviour
{
    void Update()
    {
        // Destroy the chunk map after desapear of the screen
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
}
