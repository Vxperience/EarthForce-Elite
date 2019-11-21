using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject[] niveau;
    public Sprite[] icon;
    private int niveauToLoad;

    void Start()
    {
        niveauToLoad = 1;
    }
    
    void Update()
    {
        for (int i = 0; i < niveau.Length; i++)
        {
            if (niveauToLoad == i + 1) {
                niveau[i].GetComponent<Image>().sprite = icon[1];
                niveau[i].GetComponentInChildren<Text>().color = new Color(0, 0, 0);
            } else {
                niveau[i].GetComponent<Image>().sprite = icon[0];
                niveau[i].GetComponentInChildren<Text>().color = new Color(25, 119, 151);
            }
        }
    }

    public void ChangeNiveauToLoad(int niveau)
    {
        niveauToLoad = niveau;
    }

    public void LaunchGame()
    {
        Debug.Log(niveauToLoad);
        PlayerPrefs.SetInt("Niveau", niveauToLoad);
        SceneManager.LoadScene(1);
    }
}