using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{

    public Transform thePlayer;
    public bool _active;

    void Update()
    {
        if (_active == true)
        {
            Vector3 playerPosition = new Vector3(thePlayer.position.x, transform.position.y, thePlayer.position.z);
            transform.LookAt(playerPosition);
        }
    }
}
