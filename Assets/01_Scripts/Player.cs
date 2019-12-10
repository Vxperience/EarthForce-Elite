using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject shield;
    public Rigidbody tir;
    public GameObject explosion;
    public Texture[] sprite;
    public float speedTir;
    public float reload;
    public int hit;
    public int hp;
    public bool finish;
    public bool ispause;
    private GameObject tirAudio;
    private float count;
    private bool isdead;
    private bool explode;
    private bool isAttackSpeed;
    private bool isAudio;

    void Start()
    {
        // Initialise the player
        tirAudio = new GameObject("TirAudio");
        tirAudio.transform.parent = gameObject.transform;
        tirAudio.AddComponent<AudioSource>().clip = GameObject.Find("Audio").GetComponent<Audio>().lightFire[4];

        speedTir = 10f;
        reload = 0.5f;
        hit = 5;
        hp = 3;
        count = 0;
        isdead = false;
        explode = false;
        finish = false;
        isAttackSpeed = false;
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;
        StartCoroutine(Fire());
    }
    
    void Update()
    {
        // Decrease the counter of the AttackSpeed power-up
        if (count > 0)
            count -= Time.deltaTime;

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
            else {
                if (!explode) {
                    Instantiate(explosion, new Vector3(transform.position.x + 0.05f, transform.position.y - 1.5f, transform.position.z), Quaternion.Euler(0, 0, 0), transform);
                    explode = true;
                }
                transform.position += new Vector3(0, -1 * Time.deltaTime, 0);
            }
        }
    }

    IEnumerator Fire()
    {
        // Manage fire of the player
        if (isAudio)
            tirAudio.GetComponent<AudioSource>().Play();
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
                    count = 5;
                    StartCoroutine(AttackSpeed());
                }
                break;
            case "Shield":
                shield.SetActive(true);
                break;
            default:
                if (shield.activeSelf) {
                    if (collision.gameObject.tag == "Ennemi") {
                        if (collision.gameObject.name.Contains("pick-up"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x + 0.3f, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                        if (collision.gameObject.name.Contains("moto"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                        if (collision.gameObject.name.Contains("lanceur"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x + 0.3f, collision.transform.position.y + 0.2f, collision.transform.position.z), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                    }
                    shield.SetActive(false);
                } else {
                    if (collision.gameObject.tag == "Ennemi") {
                        if (collision.gameObject.name.Contains("pick-up"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x + 0.3f, collision.transform.position.y + 0.5f, collision.transform.position.z - 1), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                        if (collision.gameObject.name.Contains("moto"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                        if (collision.gameObject.name.Contains("lanceur"))
                            Instantiate(explosion, new Vector3(collision.transform.position.x + 0.3f, collision.transform.position.y + 0.2f, collision.transform.position.z), Quaternion.Euler(0, 0, 0), collision.transform.parent);
                    }
                    hp--;
                }
                break;
        }
        Destroy(collision.gameObject);
    }

    IEnumerator AttackSpeed()
    {
        // Give the AttackSpeed boost
        reload *= 0.5f;
        yield return new WaitForSeconds(5);
        reload *= 2;
        isAttackSpeed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect the Endgame
        if (other.tag == "EndGame") {
            finish = true;
            GameObject.Find("Fin").GetComponent<Text>().text = "Victory";
            if (GameObject.Find("Niveau1") && PlayerPrefs.GetInt("Progression") < 2)
                PlayerPrefs.SetInt("Progression", 2);
            if (GameObject.Find("Niveau2") && PlayerPrefs.GetInt("Progression") < 3)
                PlayerPrefs.SetInt("Progression", 3);
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        // Return to menu
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EFE_menu");
    }

    private void OnGUI()
    {
        // Show the remaining time of the AttackSpeed power-up
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.skin.box.fontSize = 120;
        if (isAttackSpeed && !ispause) {
            GUI.DrawTexture(new Rect(20, 20, 160, 160), sprite[0]);
            GUI.color = new Color(0, 0, 0, 1f);
            GUI.Box(new Rect(190, 20, 320, 160), count.ToString("0.00"));
        }
    }
}
