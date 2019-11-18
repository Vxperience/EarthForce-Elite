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
        ispause = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ispause = false;
        if (Input.GetMouseButtonUp(0))
            ispause = true;
        Player.GetComponent<Player>().ispause = ispause;
        Pause.SetActive(ispause);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
