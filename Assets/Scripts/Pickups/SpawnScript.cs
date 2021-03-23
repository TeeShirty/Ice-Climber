using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject[] spawnPickups;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnPickups[Random.Range(0, spawnPickups.Length)], this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
