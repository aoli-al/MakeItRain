using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBloodPos : MonoBehaviour
{

    public Transform objectToFollow;
    public float y;
    // Update is called once per frame

    void LateUpdate()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule editableShape = ps.shape;
        var pos = -objectToFollow.position;
        editableShape.position = new Vector3(pos.x, y, pos.z);
        Debug.Log(objectToFollow.position);
        Debug.Log(transform.position);
    }
}
