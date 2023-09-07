using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager : MonoBehaviour
{
    public SteeringSeek seek;
    public SteeringWander wander;
    Vector3 velocity;
    public float radius;
    public Transform avoid;
    public bool isWandering;
    // Start is called before the first frame update
    void Awake()
    {
        velocity = transform.right;
    }

    // Update is called once per frame
    void Update()
    {

        if (isWandering == false)
        {
            velocity += wander.Wander(transform, velocity) + velocity;
            velocity = velocity.normalized * 5f;
            StartCoroutine(timer());

            //print(isWandering);
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        /*        if (Vector3.Distance(transform.position, mousePos) > radius)
                {
                    //normal speed
                    velocity = seek.Seek(mousePos, velocity, 1.5f) + velocity;
                    velocity = velocity.normalized * 5f;
                }
                else
                {
                    //slow down
                    velocity = seek.Seek(mousePos, velocity, 1.5f) + velocity;
                    velocity = velocity.normalized * 5f * Vector3.Distance(transform.position, mousePos) / radius;
                }*/
        
        if(Vector3.Distance(transform.position, avoid.position) < radius )
        {
            velocity += seek.Avoid(avoid.position, velocity, 1.4f);
            velocity = velocity.normalized * 5f;
        }

        transform.position = transform.position + velocity * Time.deltaTime;


    }

    public IEnumerator timer()
    {
        isWandering = true;
        yield return new WaitForSeconds(0.5f);
        isWandering = false;
    }
    public void EvokeWander()
    {
        velocity += wander.Wander(transform, velocity);
        velocity = velocity.normalized * 5f;
    }
}
