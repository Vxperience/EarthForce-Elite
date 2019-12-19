using System.Collections;
using UnityEngine;

public class EFE_Boss : MonoBehaviour
{
    public GameObject endGame;
    public GameObject explosion;
    public float posx;
    public int part;
    public int hp;
    private GameObject player;
    private int move;
    private bool isactive;
    private bool isdead;

    void Start()
    {
        // initialise the boss
        player = GameObject.Find("CharLeclerc");
        isactive = false;
        isdead = false;
    }
    
    void Update()
    {
        // Check if it lives
        if (hp <= 0 && !isdead) {
            isdead = true;
            StartCoroutine(Dead());
        }

        if (transform.position.y < 5 && !isactive) {
            isactive = true;
            StartCoroutine(NextMove());
        }

        // Manage move of the boss
        if (!isdead && isactive) {
            endGame.transform.position += new Vector3(0, 1.5f * Time.deltaTime, 0);
            transform.position += new Vector3(0, 1.5f * Time.deltaTime, 0);
            if (move == 1 && transform.position.x < posx)
                transform.position += new Vector3(0.2f * Time.deltaTime, 0, 0);
            else if (move == 2 && transform.position.x > -posx)
                transform.position += new Vector3(-0.2f * Time.deltaTime, 0, 0);
        }
    }

    IEnumerator Dead()
    {
        // Manage death
        Instantiate(explosion, new Vector3(transform.position.x - 0.4f, transform.position.y - 0.2f, transform.position.z), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion, new Vector3(transform.position.x + 0.2f, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }

    IEnumerator NextMove()
    {
        // Manage the next move direction / 0 nothing | 1 right | 2 left \
        move = Random.Range(0, 3);
        yield return new WaitForSeconds(3);
        StartCoroutine(NextMove());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Manage collision
        if (collision.gameObject.name == "CharLeclerc")
            player.GetComponent<EFE_Player>().hp -= 3;
        if (part == 0) {
            hp -= player.GetComponent<EFE_Player>().hit;
        }
    }
}
