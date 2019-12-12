using System.Collections;
using UnityEngine;

public class EFE_Ennemi : MonoBehaviour
{
    public Rigidbody tir;
    public int hp;
    private GameObject menuGame;
    private GameObject tirAudio;
    private bool firstfire;
    private bool ispause;
    private bool isAudio;

    void Start()
    {
        // Initialise the ennemi
        tirAudio = new GameObject("TirAudio");
        tirAudio.transform.parent = gameObject.transform;

        hp = 10;
        menuGame = GameObject.Find("MenuGame");
        firstfire = false;
        ispause = menuGame.GetComponent<EFE_MenuGame>().ispause;
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;
    }
    
    void Update()
    {
        ispause = menuGame.GetComponent<EFE_MenuGame>().ispause;

        // Manage the comportement of the ennemi
        if (gameObject.name.Contains("pick-up")) {
            if (transform.position.y < 7 && !firstfire) {
                tirAudio.AddComponent<AudioSource>().clip = GameObject.Find("Audio").GetComponent<EFE_Audio>().lightFire[3];
                StartCoroutine(Fire(3, 10f));
                firstfire = true;
            }
        } else if (gameObject.name.Contains("moto")) {
            if (transform.position.y < 7 && !ispause)
                transform.position += new Vector3(0, -4 * Time.deltaTime, 0);
        } else if (gameObject.name.Contains("lanceur")) {
            if (transform.position.y < 7 && !firstfire) {
                tirAudio.AddComponent<AudioSource>().clip = GameObject.Find("Audio").GetComponent<EFE_Audio>().heavyFire[0];
                StartCoroutine(Fire(5, 4f));
                firstfire = true;
            }
        }

        // Destroy the gameobject when it disapear of the screen
        if (transform.position.y < -7)
            Destroy(gameObject);
    }

    IEnumerator Fire(float reload, float speedTir)
    {
        // Manage the fire of the ennemi
        if (isAudio)
            tirAudio.GetComponent<AudioSource>().Play();
        Rigidbody t = Instantiate(tir, new Vector3(transform.position.x + 0.35f, transform.position.y - 0.5f, transform.position.z - 0.5f), Quaternion.Euler(-270, -240, -60));
        t.velocity = transform.forward * speedTir;
        yield return new WaitForSeconds(reload);
        StartCoroutine(Fire(reload, speedTir));
    }
}
