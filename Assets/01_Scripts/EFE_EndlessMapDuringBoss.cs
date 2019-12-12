using UnityEngine;

public class EFE_EndlessMapDuringBoss : MonoBehaviour
{
    public GameObject map;
    public float posy;
    private GameObject newMap;
    private GameObject niveau;

    private void Start()
    {
        niveau = GameObject.Find("Décor");
    }

    void Update()
    {
        // Initialise the next chunk of the map
        if (transform.position.y < posy) {
            // Map
            newMap = Instantiate(map, new Vector3(4.25f, posy + 10, 3), Quaternion.Euler(-90, 0, 0), niveau.transform);
            newMap.AddComponent<EFE_DestroyMap>();

            transform.position += new Vector3(0, 10, 0);
        }

    }
}
