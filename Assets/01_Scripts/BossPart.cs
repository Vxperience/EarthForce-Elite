using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{
    public GameObject boss;
    public GameObject explosion;
    public Rigidbody tir;
    public float[] pos = new float[3];
    public float reload;
    public float speedTir;
    public int hp;
    private GameObject player;
    private GameObject tirAudio;
    private bool isAudio;
    private bool firstfire;

    void Start()
    {
        // initialise the part of the boss
        tirAudio = new GameObject("TirAudio");
        tirAudio.transform.parent = gameObject.transform;
        player = GameObject.Find("CharLeclerc");
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;
        firstfire = false;
    }

    void Update()
    {
        // Check if it start fire
        if (transform.position.y < 7 && !firstfire) {
            tirAudio.AddComponent<AudioSource>().clip = GameObject.Find("Audio").GetComponent<Audio>().lightFire[3];
            StartCoroutine(Fire(reload, speedTir));
            firstfire = true;
        }

        // Check if it lives
        if (hp <= 0) {
            boss.GetComponent<Boss>().part--;
            Instantiate(explosion, new Vector3(transform.position.x + pos[0], transform.position.y + pos[1], transform.position.z + pos[2]), Quaternion.Euler(0, 0, 0), boss.transform);
            Destroy(gameObject);
        }
    }

    IEnumerator Fire(float reload, float speedTir)
    {
        // Manage the fire of the ennemi
        int count = 1;

        if (isAudio)
            tirAudio.GetComponent<AudioSource>().Play();
        if (gameObject.name != "Boss-2")
            count = 3;
        for (int i = 0; i < count; i++)
        {
            Rigidbody t;
            switch (gameObject.name)
            {
                case "Boss-0":
                    t = Instantiate(tir, new Vector3(transform.position.x + pos[0] + 0.05f, transform.position.y + pos[1] - 1, transform.position.z + pos[2]), Quaternion.Euler(-270, -240, -60));
                    break;
                case "Boss-1":
                    t = Instantiate(tir, new Vector3(transform.position.x + pos[0] - 0.15f, transform.position.y + pos[1] - 1, transform.position.z + pos[2]), Quaternion.Euler(-270, -240, -60));
                    break;
                default:
                    t = Instantiate(tir, new Vector3(transform.position.x + pos[0], transform.position.y + pos[1] - 2.5f, transform.position.z + pos[2]), Quaternion.Euler(-270, -240, -60));
                    break;
            }
            t.velocity = transform.forward * speedTir;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(reload);
        StartCoroutine(Fire(reload, speedTir));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Manage collision
        if (!collision.gameObject.name.Contains("Boss")) {
            hp -= player.GetComponent<Player>().hit;
            if (collision.gameObject.name == "CharLeclerc")
                player.GetComponent<Player>().hp -= 3;
        }
    }
}
