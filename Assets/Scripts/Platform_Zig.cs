using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Zig : MonoBehaviour {

    public float speed = 1;

    private int direction = 1;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZigZag")
        {
            if (direction == 1)

                direction = -1;



            else
                direction = 1;
        }

    }
}
