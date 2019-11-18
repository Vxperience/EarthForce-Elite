using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int niveauToLoad;

    void Start()
    {
        niveauToLoad = 0;
    }
    
    void Update()
    {
        
    }

    public void ChangeNiveauToLoad(int niveau)
    {
        niveauToLoad = niveau;
    }

    public void LaunchGame()
    {
        Debug.Log(niveauToLoad);
        SceneManager.LoadScene(1);
    }
}