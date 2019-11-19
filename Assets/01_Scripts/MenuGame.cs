using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public GameObject Player;
    public GameObject Pause;
    private bool ispause;
    
    void Start()
    {
        Time.timeScale = 0.25f;
        ispause = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            ispause = false;
            Time.timeScale = 1f;
        }
        if (Input.GetMouseButtonUp(0)) {
            ispause = true;
            Time.timeScale = 0.25f;
        }
        Player.GetComponent<Player>().ispause = ispause;
        Pause.SetActive(ispause);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
