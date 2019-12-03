using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niveau : MonoBehaviour
{
    public GameObject menuGame;
    public bool ispause;
    
    void Update()
    {
        // Manage the pause of the game
        ispause = menuGame.GetComponent<MenuGame>().ispause;
        if (!ispause)
            transform.position += new Vector3(0, -1.5f * Time.deltaTime, 0);
    }
}
