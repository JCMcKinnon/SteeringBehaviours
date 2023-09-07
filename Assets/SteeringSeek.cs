using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeek : MonoBehaviour
{
    public Vector3 Seek(Vector3 targetPosition, Vector3 currentVelocity, float mass)
    {
        var desiredVelocity = Vector3.Normalize(targetPosition - gameObject.transform.position) * 6f;
        var steering = desiredVelocity - currentVelocity;
        steering = steering.normalized * 6f;
        steering = steering / mass;
        
        return steering;
    }
    public Vector3 Avoid(Vector3 targetPosition, Vector3 currentVelocity, float mass)
    {
        var desiredVelocity = Vector3.Normalize( gameObject.transform.position - targetPosition) * 6f;
        var steering = desiredVelocity - currentVelocity;
        steering = steering.normalized * 6f;
        steering = steering / mass;

        return steering;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
