using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public GameObject[] niveau;
    public GameObject player;
    public GameObject pause;
    public GameObject menu;
    public GameObject[] hp;
    public Sprite[] spriteHp;
    public bool ispause;
    
    void Start()
    {
        switch (PlayerPrefs.GetInt("Niveau")) {
            case 0:
                niveau[0].SetActive(true);
                break;
            case 1:
                niveau[1].SetActive(true);
                break;
            case 2:
                niveau[2].SetActive(true);
                break;
            case 3:
                niveau[3].SetActive(true);
                break;
        }
        Time.timeScale = 0f;
        ispause = true;
    }
    
    void Update()
    {
        // Check if the game is in pause
        if ((Input.mousePosition.x <= menu.transform.position.x - 15 || Input.mousePosition.x >= menu.transform.position.x + 15) && (Input.mousePosition.y <= menu.transform.position.y - 15 || Input.mousePosition.y >= menu.transform.position.y + 15) && !player.GetComponent<Player>().finish) {
            if (Input.GetMouseButtonDown(0)) {
                ispause = false;
                Time.timeScale = 1f;
            }
            if (Input.GetMouseButtonUp(0)) {
                ispause = true;
                Time.timeScale = 0f;
            }
        }
        player.GetComponent<Player>().ispause = ispause;
        pause.SetActive(ispause);

        // Manage the pause menu
        if (ispause) {
            for (int i = 0; i < hp.Length; i++) {
                if (player.GetComponent<Player>().hp >= i + 1)
                    hp[i].GetComponent<Image>().sprite = spriteHp[0];
                else
                    hp[i].GetComponent<Image>().sprite = spriteHp[1];
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("EFE_menu");
    }

    // Show FPS
    /*void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), "FPS: " + (int)(1.0f / Time.smoothDeltaTime));
    }*/
}
