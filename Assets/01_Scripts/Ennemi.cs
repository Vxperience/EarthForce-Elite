using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public Rigidbody tir;
    public int hp;
    private bool firstfire;

    void Start()
    {
        hp = 10;
        firstfire = false;
    }
    
    void Update()
    {
        if (transform.position.y < 7 && !firstfire) {
            StartCoroutine(Fire(3, 10f));
            firstfire = true;
        }
    }

    IEnumerator Fire(float reload, float speedTir)
    {
        Rigidbody t = Instantiate(tir, new Vector3(transform.position.x + 0.35f, transform.position.y - 0.5f, transform.position.z - 0.5f), Quaternion.Euler(-270, -240, -60));
        t.velocity = transform.forward * speedTir;
        yield return new WaitForSeconds(reload);
        StartCoroutine(Fire(reload, speedTir));
    }
}
