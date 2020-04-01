using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public GameObject oggettoDaSpostare;
    public Transform puntoIniziale;
    public Transform puntoFinale;
    public float velocita;
    private Vector3 target; // prossimo obiettivo per l'oggetto
    // Start is called before the first frame update
    void Start()
    {
        target = puntoFinale.position;
    }

    // Update is called once per frame
    void Update()
    {
        oggettoDaSpostare.transform.position = Vector3.MoveTowards(oggettoDaSpostare.transform.position, target, velocita * Time.deltaTime);
        if(oggettoDaSpostare.transform.position == puntoFinale.position)
        {
            target = puntoIniziale.position;
        }
        if (oggettoDaSpostare.transform.position == puntoIniziale.position)
        {
            target = puntoFinale.position;
        }
    }
}
