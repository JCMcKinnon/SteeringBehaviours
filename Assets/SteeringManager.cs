using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager : MonoBehaviour
{
    public SteeringSeek seek;
    public SteeringWander wander;
    public SteeringCollisionAvoid collisionAvoid;
    Vector3 velocity;
    public float radius;
    public Transform avoid;
    public bool isWandering;
    public bool canAvoidWall;
    float turnTimer;
    Collider col;
    States boidState = States.wander;
    public GameObject[] walls;
    public Vector3[] obstacles;
    float t = 0;
    Coroutine coroutine;

    public GameObject[] objs;
    enum States
    {
        avoidWall,
        wander
    }
    // Start is called before the first frame update
    void Awake()
    {
        velocity = transform.right;
        collisionAvoid = GetComponent<SteeringCollisionAvoid>();
        objs = GameObject.FindGameObjectsWithTag("Player");
        obstacles = new Vector3[100];
        for (int i = 0; i < objs.Length; i++)
        {
            obstacles[i] = objs[i].transform.position;
        }
        walls = new GameObject[6];
        isWandering = true;
        // isWandering = false;
        canAvoidWall = false;
    }
    private void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("wall");
        turnTimer = 0.25f;
    }
    // Update is called once per frame
    void Update()
    {
        FaceDirection();
        RaycastHit hit = new RaycastHit();
        var cast = Physics.Raycast(transform.position + transform.up, transform.up, out hit, 5f);
        turnTimer -= Time.deltaTime;


        velocity += collisionAvoid.AvoidCollisions(velocity,5f,3f,obstacles);
        velocity = velocity.normalized * 10f;


        switch (boidState)
        {
            case States.wander:
                if(turnTimer <= 0) {
                    velocity += wander.Wander(transform, velocity) + velocity;
                    velocity = velocity.normalized * 5f;
                    turnTimer = 0.25f;
                }
             
                if (cast)
                {
                    if(hit.collider.tag == "wall")
                    {
                        boidState = States.avoidWall;
                        
                    }
                }
                break;
            case States.avoidWall:
                if(cast)
                {
                    velocity += seek.Seek(hit.collider.transform.position - transform.position, velocity, 1f);
                    velocity = velocity.normalized * 5f;
                }
                boidState = States.wander;

                break;


        }





        transform.position = transform.position + velocity * Time.deltaTime;


    }

    public IEnumerator timer()
    {
        velocity += wander.Wander(transform, velocity) + velocity;
        velocity = velocity.normalized * 5f;

        yield return new WaitForSeconds(0.2f);


    }
    public void EvokeWander()
    {
        velocity += wander.Wander(transform, velocity);
        velocity = velocity.normalized * 5f;
    }

    public void FaceDirection()
    {
        t += Time.deltaTime * 4;
        Mathf.Clamp01(t);
        turnTimer -= Time.deltaTime;
        transform.up = velocity.normalized;
        transform.up = Vector3.Lerp(transform.up, velocity.normalized, t);
        if (transform.up == velocity.normalized)
        {
            t = 0;
        }
    }
}
