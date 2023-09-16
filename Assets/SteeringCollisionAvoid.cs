using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringCollisionAvoid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Vector3 AvoidCollisions(Vector3 velocity, float MAX_SEE_AHEAD, float radius, Vector3[] obstacles)
    {
        //for every obstacle in the scene
        //check if player + velocity.normalized * max see ahead
        //is less than or equal to a sphere radius around the obstacle
        //if it is a collision was found

        //to do
        //make argument for every obstacle 
        //make argument for velocity
        //make argument for max see ahead
        //make argument for radius
        var lookAheadVector = transform.position + velocity.normalized * MAX_SEE_AHEAD;
        for (int i = 0; i < obstacles.Length; i++)
        {
            var distanceFromLookAheadToObstacle = Vector3.Distance(lookAheadVector, obstacles[i]);


            if (distanceFromLookAheadToObstacle <= radius)
            {
                //create vector pointing away from obstacle TO the lookahead vector
                var avoidVector = lookAheadVector - obstacles[i];
                var payloadVector =  avoidVector + velocity.normalized;
                payloadVector.Normalize();
                print("avoiding");
                return payloadVector;
            }
        }
        return Vector3.zero;
    }

}
