using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject shield;
    public Rigidbody tir;
    public float speedTir;
    public float reload;
    public int hit;
    public int hp;
    public bool finish;
    public bool ispause;
    private bool isdead;
    private bool isAttackSpeed;

    void Start()
    {
        speedTir = 10f;
        reload = 0.5f;
        hit = 5;
        hp = 3;
        isdead = false;
        finish = false;
        isAttackSpeed = false;
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
                if (hit.point.x > transform.position.x && hit.point.x - 0.1 > transform.position.x && transform.position.x <= 2 && transform.position.x + 0.1 <= 2)
                    transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
                else if (hit.point.x < transform.position.x && hit.point.x + 0.1 < transform.position.x && transform.position.x >= -2 &&  transform.position.x - 0.1 >= -2)
                    transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
                if (hit.point.y + 2 > transform.position.y && hit.point.y + 1.9 > transform.position.y)
                    transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
                else if (hit.point.y - 2 < transform.position.y && hit.point.y + 2.1 < transform.position.y)
                    transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
            }
        }

        // Manage Endgame
        if (finish) {
            if (!isdead)
                transform.position += new Vector3(0, 4 * Time.deltaTime, 0);
            else
                transform.position += new Vector3(0, -1 * Time.deltaTime, 0);
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
        // Manage the collision of the player
        switch (collision.gameObject.tag)
        {
            case "HP":
                if (hp < 3)
                    hp++;
                break;
            case "AttackSpeed":
                if (!isAttackSpeed) {
                    isAttackSpeed = true;
                    StartCoroutine(AttackSpeed());
                }
                break;
            case "Shield":
                shield.SetActive(true);
                break;
            default:
                if (shield.activeSelf)
                    shield.SetActive(false);
                else
                    hp--;
                break;
        }
        Destroy(collision.gameObject);
    }

    IEnumerator AttackSpeed()
    {
        reload *= 0.5f;
        yield return new WaitForSeconds(5);
        reload *= 2;
        isAttackSpeed = false;
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
        SceneManager.LoadScene("EFE_menu");
    }
}
