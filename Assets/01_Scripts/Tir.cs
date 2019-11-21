using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    private GameObject player;
    private int hit;

    void Start()
    {
        player = GameObject.Find("CharLeclerc");
        hit = player.GetComponent<Player>().hit;
    }
    
    void Update()
    {
        if (transform.position.y > 7 || transform.position.y < -7)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ennemi") {
            collision.transform.gameObject.GetComponent<Ennemi>().hp -= hit;
            if (collision.transform.gameObject.GetComponent<Ennemi>().hp <= 0)
                Destroy(collision.transform.gameObject);
            Destroy(gameObject);
        }
    }
}
