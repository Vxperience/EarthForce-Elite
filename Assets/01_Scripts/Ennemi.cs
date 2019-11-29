using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public Rigidbody tir;
    public int hp;
    private GameObject menuGame;
    private bool firstfire;
    private bool ispause;

    void Start()
    {
        hp = 10;
        menuGame = GameObject.Find("MenuGame");
        firstfire = false;
        ispause = menuGame.GetComponent<MenuGame>().ispause;
    }
    
    void Update()
    {
        ispause = menuGame.GetComponent<MenuGame>().ispause;

        if (gameObject.name.Contains("pick-up")) {
            if (transform.position.y < 7 && !firstfire) {
                StartCoroutine(Fire(3, 10f));
                firstfire = true;
            }
        } else if (gameObject.name.Contains("moto")) {
            if (transform.position.y < 7 && !ispause)
                transform.position += new Vector3(0, -0.06f, 0);
        } else if (gameObject.name.Contains("lanceur")) {
            if (transform.position.y < 7 && !firstfire) {
                StartCoroutine(Fire(5, 4f));
                firstfire = true;
            }
        }

        //Destroy when it disapear of the screen
        if (transform.position.y < -7)
            Destroy(gameObject);
    }

    IEnumerator Fire(float reload, float speedTir)
    {
        Rigidbody t = Instantiate(tir, new Vector3(transform.position.x + 0.35f, transform.position.y - 0.5f, transform.position.z - 0.5f), Quaternion.Euler(-270, -240, -60));
        t.velocity = transform.forward * speedTir;
        yield return new WaitForSeconds(reload);
        StartCoroutine(Fire(reload, speedTir));
    }
}
