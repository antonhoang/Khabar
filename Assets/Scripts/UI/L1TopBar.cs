using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1TopBar : MonoBehaviour
{

    void Start()
    {
        // Get the current position and change only the Y component.
        //Vector3 newPosition = transform.position;
        //newPosition.y = 0;

        //// Set the new position.
        //transform.position = newPosition;
        print("1" + transform.position.y);
        transform.position = new Vector3(transform.position.x, transform.position.y + (float)1.3, transform.position.z);
        print("2" + transform.position.y);
    }

    private void Update()
    {
        //Vector3 newPosition = transform.position;
        //newPosition.y = 0;

        //// Set the new position.
        //transform.position = newPosition;
    }
}
