using UnityEngine;

public class EFE_Tir : MonoBehaviour
{
    public GameObject[] powerUp;
    public GameObject explosion;
    private GameObject anchor;
    private GameObject player;
    private int hit;
    private bool ispause;

    void Start()
    {
        // Initialise the shot
        anchor = GameObject.Find("Décor");
        player = GameObject.Find("CharLeclerc");
        hit = player.GetComponent<EFE_Player>().hit;
        ispause = player.GetComponent<EFE_Player>().ispause;
    }
    
    void Update()
    {
        ispause = player.GetComponent<EFE_Player>().ispause;

        // if it's shot by a "lanceur" the missile have to follow the player
        if (gameObject.name.Contains("tir02") && !ispause) {
            if (player.transform.position.y - 2 < transform.position.y) {
                if (player.transform.position.x < transform.position.x)
                    transform.position += new Vector3(-0.8f * Time.deltaTime, 0, 0);
                else if (player.transform.position.x > transform.position.x)
                    transform.position += new Vector3(0.8f * Time.deltaTime, 0, 0);
            }
        }

        // Destroy the shoot if its out of the screen
        if (transform.position.y > 7 || transform.position.y < -7)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Manage collision of the shot
        if (collision.transform.tag != "Fire")
        {
            if (collision.transform.tag == "Ennemi")
            {
                collision.transform.gameObject.GetComponent<EFE_Ennemi>().hp -= hit;
                if (collision.transform.gameObject.GetComponent<EFE_Ennemi>().hp <= 0)
                {
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
}
