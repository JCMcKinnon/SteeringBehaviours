using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager : MonoBehaviour
{
    public SteeringSeek seek;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        velocity = seek.Seek(mousePos,velocity,2) + velocity;
        velocity = velocity.normalized * 3f;
        transform.position = transform.position + velocity * Time.deltaTime;
    }
}
