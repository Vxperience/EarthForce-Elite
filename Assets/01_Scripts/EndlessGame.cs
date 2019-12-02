using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessGame : MonoBehaviour
{
    public GameObject niveau;
    public GameObject[] map;
    public GameObject[] ennemi;
    private GameObject newMap;

    void Start()
    {
        for (int i = 0; i < 2; i++) {
            newMap = Instantiate(map[Random.Range(0, map.Length)], new Vector3(4.25f, i * 10, 3), Quaternion.Euler(-90, 0, 0), niveau.GetComponent<Transform>().Find("Décor").transform);
            newMap.AddComponent<DestroyMap>();

            if (i != 0) {
                List<float> listEnnemiPosX = new List<float>();
                int nbennemi = Random.Range(1, 4);
                for (int j = 0; j < nbennemi; j++)
                {
                    int indicEnnemi = Random.Range(0, ennemi.Length);
                    float x = Random.Range(-2.000f, 0.800f);
                    float y = Random.Range(0.000f, 8.500f);
                    switch (indicEnnemi)
                    {
                        case 0:
                            while (!CheckCollisionWithOther(x, listEnnemiPosX, 0.7f))
                            {
                                x = Random.Range(-2.000f, 0.800f);
                            }
                            listEnnemiPosX.Add(x);
                            listEnnemiPosX.Add(x + 0.7f);
                            Instantiate(ennemi[indicEnnemi], new Vector3(x, y + i * 10, 1.125f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                            break;
                        case 1:
                            while (!CheckCollisionWithOther(x, listEnnemiPosX, 0.35f))
                            {
                                x = Random.Range(-2.000f, 0.800f);
                            }
                            listEnnemiPosX.Add(x);
                            listEnnemiPosX.Add(x + 0.35f);
                            Instantiate(ennemi[indicEnnemi], new Vector3(x, y + i * 10, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                            Instantiate(ennemi[indicEnnemi], new Vector3(x, y + i * 10 + 0.2f, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                            Instantiate(ennemi[indicEnnemi], new Vector3(x, y + i * 10 + 0.4f, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                            break;
                        case 2:
                            while (!CheckCollisionWithOther(x, listEnnemiPosX, 1.1f))
                            {
                                x = Random.Range(-2.000f, 0.800f);
                            }
                            listEnnemiPosX.Add(x);
                            listEnnemiPosX.Add(x + 1.1f);
                            Instantiate(ennemi[indicEnnemi], new Vector3(x, y + i * 10, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                            break;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (transform.position.y <= 20) {
            newMap = Instantiate(map[Random.Range(0, map.Length)], new Vector3(4.25f, 20, 3), Quaternion.Euler(-90, 0, 0), niveau.GetComponent<Transform>().Find("Décor").transform);
            newMap.AddComponent<DestroyMap>();

            List<float> listEnnemiPosX = new List<float>();
            int nbennemi = Random.Range(1, 4);
            for (int i = 0; i < nbennemi; i++) {
                int indicEnnemi = Random.Range(0, ennemi.Length);
                float x = Random.Range(-2.000f, 0.800f);
                float y = Random.Range(0.000f, 8.500f);
                switch (indicEnnemi) {
                    case 0:
                        while (!CheckCollisionWithOther(x, listEnnemiPosX, 0.7f)) {
                            x = Random.Range(-2.000f, 0.800f);
                        }
                        listEnnemiPosX.Add(x);
                        listEnnemiPosX.Add(x + 0.7f);
                        Instantiate(ennemi[indicEnnemi], new Vector3(x, y + 20, 1.125f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                        break;
                    case 1:
                        while (!CheckCollisionWithOther(x, listEnnemiPosX, 0.35f)) {
                            x = Random.Range(-2.000f, 0.800f);
                        }
                        listEnnemiPosX.Add(x);
                        listEnnemiPosX.Add(x + 0.35f);
                        Instantiate(ennemi[indicEnnemi], new Vector3(x, y + 20, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                        Instantiate(ennemi[indicEnnemi], new Vector3(x, y + 20 + 0.2f, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                        Instantiate(ennemi[indicEnnemi], new Vector3(x, y + 20 + 0.4f, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                        break;
                    case 2:
                        while (!CheckCollisionWithOther(x, listEnnemiPosX, 1.1f)) {
                            x = Random.Range(-2.000f, 0.800f);
                        }
                        listEnnemiPosX.Add(x);
                        listEnnemiPosX.Add(x + 1.1f);
                        Instantiate(ennemi[indicEnnemi], new Vector3(x, y + 20, 0.7f), Quaternion.Euler(90, 180, 0), niveau.GetComponent<Transform>().Find("Ennemi").transform);
                        break;
                }
            }
            transform.position += new Vector3(0, 10, 0);
        }
    }

    bool CheckCollisionWithOther(float x, List<float> posX, float lx)
    {
        float[] posx = posX.ToArray();
        for (int i = 0; i < posx.Length; i += 2)
        {
            if (!(x < posx[i] && x < posx[i + 1] && x + lx < posx[i] && x + lx < posx[i + 1]) && !(x > posx[i] && x > posx[i + 1] && x + lx > posx[i] && x + lx > posx[i + 1]))
                return (false);
        }
        return (true);
    }
}
