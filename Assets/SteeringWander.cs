using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{
    // Start is called before the first frame update
    public SteeringSeek Seek;
    public Vector3 Wander(Transform pos, Vector3 currentVector)
    {
        //create circle in front of player.transform.pos
        //use inside unit circle
        //direction is now the direction from player to that position.
        var worldPosOfCircle = (Vector3)pos.position + (currentVector * 2 );
        var randomPoint = worldPosOfCircle + ((Vector3)Random.insideUnitCircle * 50);
        var newDirection =  (Vector3)pos.position - randomPoint;
        newDirection.Normalize();

        //every 2 seconds, repeat...(in manager)
        return newDirection * 5;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator repeatTimer(float t)
    {
        yield return new WaitForSeconds(t);
       
    }
}
