using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayer : MonoBehaviour
{

    public Transform objectToFollow;
    public Vector3 lastPose;

	void Start() {
		lastPose = objectToFollow.position;
	}
    
    // Update is called once per frame
    void LateUpdate()
    {
		// Debug.Log(Quaternion.AngleAxis(28.7f, Vector3.up) * (objectToFollow.position - lastPose));
		transform.position += Quaternion.AngleAxis(28.7f, Vector3.up) *  (objectToFollow.position - lastPose);
		//var pos = Quaternion.AngleAxis(28.7f, Vector3.up) * objectToFollow.position;
		var pos = objectToFollow.position;
		pos.y = 3.5f;
		//pos = pos + new Vector3(0.8f, 0.0f, 2.17f-5.8f);
		//pos.z *= 3.85f;
		//pos.x += 3.0f;
		transform.position = pos;
		
		lastPose = objectToFollow.position;
		Debug.Log(transform.position);
        //transform.position = new Vector3(objectToFollow.position.x, transform.position.y, objectToFollow.position.z);
    }
}
