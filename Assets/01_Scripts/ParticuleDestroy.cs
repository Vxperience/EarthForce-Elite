using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleDestroy : MonoBehaviour
{
    private bool isAudio;

    private void Start()
    {
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;

        if (isAudio)
            gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyParticle(2));
    }

    IEnumerator DestroyParticle(int timer)
    {
        // Destroy the object after a time
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
