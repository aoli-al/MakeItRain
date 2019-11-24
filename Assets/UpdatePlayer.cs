using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayer : MonoBehaviour
{

    public Transform objectToFollow;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(objectToFollow.position.x, transform.position.y, objectToFollow.position.z);
    }
}
