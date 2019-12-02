using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public GameObject[] powerUp;
    public GameObject explosion;
    private GameObject anchor;
    private GameObject player;
    private int hit;
    private bool ispause;

    void Start()
    {
        anchor = GameObject.Find("Décor");
        player = GameObject.Find("CharLeclerc");
        hit = player.GetComponent<Player>().hit;
        ispause = player.GetComponent<Player>().ispause;
    }
    
    void Update()
    {
        ispause = player.GetComponent<Player>().ispause;
        if (gameObject.name.Contains("tir02") && !ispause) {
            if (player.transform.position.x < transform.position.x)
                transform.position += new Vector3(-0.8f * Time.deltaTime, 0, 0);
            else if (player.transform.position.x > transform.position.x)
                transform.position += new Vector3(0.8f * Time.deltaTime, 0, 0);
        }

        // Destroy the shoot if its out of the screen
        if (transform.position.y > 7 || transform.position.y < -7)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ennemi") {
            collision.transform.gameObject.GetComponent<Ennemi>().hp -= hit;
            if (collision.transform.gameObject.GetComponent<Ennemi>().hp <= 0) {
                int rand = Random.Range(0, 100);
                if (rand > 10 && rand < 30)
                    Instantiate(powerUp[Random.Range(0, powerUp.Length)], new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z - 0.75f), Quaternion.Euler(0, 180, 0), GameObject.Find("Ennemi").transform);
                Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0), anchor.transform);
                Destroy(collision.transform.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
