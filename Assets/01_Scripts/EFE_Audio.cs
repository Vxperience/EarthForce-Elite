using System.Collections;
using UnityEngine;

public class EFE_Audio : MonoBehaviour
{
    public AudioClip theme;
    public AudioClip[] ambiance;
    public AudioClip[] explosion;
    public AudioClip[] farExplosion;
    public AudioClip[] vehicule;
    public AudioClip[] lightFire;
    public AudioClip[] heavyFire;
    public bool isMenu;
    private GameObject music;
    private GameObject wind;
    private GameObject gunFight;
    private GameObject ambiance1;
    private GameObject ambiance2;
    private bool isAudio;
    private bool changeAudio;

    void Start()
    {
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;

        if (isMenu) {
            music = GameObject.Find("Music");
            music.GetComponent<AudioSource>().clip = theme;
            music.GetComponent<AudioSource>().loop = true;
            if (isAudio)
                music.GetComponent<AudioSource>().Play();
        } else {
            wind = GameObject.Find("Wind");
            wind.GetComponent<AudioSource>().clip = ambiance[1];
            wind.GetComponent<AudioSource>().loop = true;
            wind.GetComponent<AudioSource>().volume = 0.25f;
            if (isAudio)
                wind.GetComponent<AudioSource>().Play();
            gunFight = GameObject.Find("GunFight");
            gunFight.GetComponent<AudioSource>().clip = ambiance[0];
            gunFight.GetComponent<AudioSource>().loop = true;
            gunFight.GetComponent<AudioSource>().volume = 1f;
            if (isAudio)
                gunFight.GetComponent<AudioSource>().Play();
            ambiance1 = GameObject.Find("Ambiance1");
            ambiance1.GetComponent<AudioSource>().volume = 1f;
            ambiance2 = GameObject.Find("Ambiance2");
            ambiance2.GetComponent<AudioSource>().volume = 1f;
            if (isAudio) {
                StartCoroutine(Ambiance1());
                StartCoroutine(Ambiance2());
            }
        }
    }

    IEnumerator Ambiance1()
    {
        ambiance1.GetComponent<AudioSource>().clip = farExplosion[Random.Range(0, farExplosion.Length)];
        ambiance1.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(Random.Range(5, 20));
        StartCoroutine(Ambiance1());
    }

    IEnumerator Ambiance2()
    {
        ambiance2.GetComponent<AudioSource>().clip = vehicule[Random.Range(0, vehicule.Length)];
        ambiance2.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(Random.Range(10, 30));
        StartCoroutine(Ambiance2());
    }

    void Update()
    {
        isAudio = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;

        if (isMenu && changeAudio && isAudio) {
            music.GetComponent<AudioSource>().Play();
            changeAudio = false;
        }
        if (isMenu && changeAudio && !isAudio) {
            music.GetComponent<AudioSource>().Stop();
            changeAudio = false;
        }
    }

    public void ChangeAudio()
    {
        changeAudio = !changeAudio;
    }
}
