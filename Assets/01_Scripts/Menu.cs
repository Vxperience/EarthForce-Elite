using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject[] niveau;
    public GameObject option;
    public Sprite[] icon;
    public Toggle audioT;
    public Toggle fps;
    private int niveauToLoad;
    private bool isOption;

    void Start()
    {
        // Initialise the menu
        niveauToLoad = -1;
        isOption = false;
        if (PlayerPrefs.GetInt("Progression") == 0)
            PlayerPrefs.SetInt("Progression", 1);
    }
    
    void Update()
    {
        // Manage sprite of the level icon (Which one is select)
        for (int i = 0; i < niveau.Length; i++) {
            if (niveauToLoad == i) {
                if (i == 0)
                    niveau[i].GetComponent<Image>().sprite = icon[3];
                else {
                    niveau[i].GetComponent<Image>().sprite = icon[1];
                    niveau[i].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }
            } else {
                if (i == 0)
                    niveau[i].GetComponent<Image>().sprite = icon[2];
                else if (i <= PlayerPrefs.GetInt("Progression")) {
                    niveau[i].GetComponent<Image>().sprite = icon[0];
                    niveau[i].GetComponentInChildren<Text>().color = new Color(25, 119, 151);
                } else {
                    niveau[i].GetComponent<Image>().sprite = icon[4];
                    niveau[i].GetComponentInChildren<Text>().text = "";
                }
            }
        }

        // manage option menu
        if (isOption) {
            PlayerPrefs.SetInt("isAudio", audioT.isOn ? 1 : 0);
            PlayerPrefs.SetInt("FPS", fps.isOn ? 1 : 0);
        }
    }

    public void Option()
    {
        // Open close the opetion menu
        isOption = !isOption;
        option.SetActive(isOption);
        if (isOption)
            audioT.isOn = PlayerPrefs.GetInt("isAudio") == 1 ? true : false;
    }

    public void ChangeNiveauToLoad(int niveau)
    {
        // Click on an icon to change the level to charge
        if (PlayerPrefs.GetInt("Progression") >= niveau)
            niveauToLoad = niveau;
    }

    public void LaunchGame()
    {
        // Launch the Level scene with the good level
        if (niveauToLoad >= 0) {
            Debug.Log(niveauToLoad);
            PlayerPrefs.SetInt("Niveau", niveauToLoad);
            SceneManager.LoadScene("EFE_level");
        }
    }

    public void Quitter()
    {
        // Quit EarthFoce Elite
        PlayerPrefs.DeleteKey("Progression");
        PlayerPrefs.DeleteKey("Niveau");
        PlayerPrefs.DeleteKey("isAudio");
        PlayerPrefs.DeleteKey("FPS");
        SceneManager.LoadScene(1);
    }
}