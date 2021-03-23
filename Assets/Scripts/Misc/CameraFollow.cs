using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 3.5f, transform.position.z);
        }
    }

}
