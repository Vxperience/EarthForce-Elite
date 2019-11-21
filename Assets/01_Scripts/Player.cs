using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody tir;
    public float speedTir;
    public float reload;
    public int hit;
    public int hp;
    public bool finish;
    public bool ispause;
    private bool isdead;

    void Start()
    {
        speedTir = 10f;
        reload = 0.5f;
        hit = 5;
        hp = 3;
        isdead = false;
        finish = false;
        StartCoroutine(Fire());
    }
    
    void Update()
    {
        // Check if it live
        if (hp <= 0) {
            isdead = true;
            finish = true;
            GameObject.Find("Fin").GetComponent<Text>().text = "Game Over";
            StartCoroutine(Finish());
        }

        // Move of the player
        if (!ispause && !isdead && !finish) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                if (hit.point.x > transform.position.x && hit.point.x - 0.1 > transform.position.x)
                    transform.position += new Vector3(0.035f, 0, 0);
                else if (hit.point.x < transform.position.x && hit.point.x + 0.1 < transform.position.x)
                    transform.position += new Vector3(-0.035f, 0, 0);
                if (hit.point.y + 2 > transform.position.y && hit.point.y + 1.9 > transform.position.y)
                    transform.position += new Vector3(0, 0.035f, 0);
                else if (hit.point.y - 2 < transform.position.y && hit.point.y + 2.1 < transform.position.y)
                    transform.position += new Vector3(0, -0.035f, 0);
            }
        }

        if (finish) {
            if (!isdead)
                transform.position += new Vector3(0, 0.04f, 0);
        }
    }

    IEnumerator Fire()
    {
        Rigidbody t = Instantiate(tir, new Vector3(transform.position.x + 0.05f, transform.position.y - 1f, transform.position.z - 0.065f), Quaternion.Euler(-90, 0, 0));
        t.velocity = transform.up * speedTir;
        yield return new WaitForSeconds(reload);
        if (!finish)
            StartCoroutine(Fire());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        hp--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGame") {
            finish = true;
            GameObject.Find("Fin").GetComponent<Text>().text = "Victory";
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
