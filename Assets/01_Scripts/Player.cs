using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody tir;
    public float speedTir;
    public float reload;
    public bool ispause;

    void Start()
    {
        speedTir = 10f;
        reload = 0.5f;
        StartCoroutine(Fire());
    }
    
    void Update()
    {
        if (!ispause) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                if (hit.point.x > transform.position.x && hit.point.x - 0.1 > transform.position.x)
                    transform.position += new Vector3(0.025f, 0, 0);
                else if (hit.point.x < transform.position.x && hit.point.x + 0.1 < transform.position.x)
                    transform.position += new Vector3(-0.025f, 0, 0);
                if (hit.point.y + 2 > transform.position.y && hit.point.y + 1.9 > transform.position.y)
                    transform.position += new Vector3(0, 0.025f, 0);
                else if (hit.point.y - 2 < transform.position.y && hit.point.y + 2.1 < transform.position.y)
                    transform.position += new Vector3(0, -0.025f, 0);
            }
        }
    }

    IEnumerator Fire()
    {
        Rigidbody t = Instantiate(tir, new Vector3(transform.position.x + 0.05f, transform.position.y - 1f, transform.position.z - 0.065f), Quaternion.Euler(-90, 0, 0));
        t.velocity = transform.up * speedTir;
        yield return new WaitForSeconds(reload);
        StartCoroutine(Fire());
    }
}
