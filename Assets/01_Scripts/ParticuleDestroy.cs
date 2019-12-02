using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleDestroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyParticle(2));
    }

    IEnumerator DestroyParticle(int timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
