using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int startingLives;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.currentLevel = GetComponent<LevelManager>();
        GameManager.instance.lives = startingLives;
        GameManager.instance.spawnPlayer(spawnLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
