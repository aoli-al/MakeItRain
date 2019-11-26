using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBloodDecayPos : MonoBehaviour
{

    public GameObject objectToFollow;
    private Vector3 initPos;
    private Vector3 initObjectPos;

    public bool shouldUpdate = false;

    void Start() {
        initPos = transform.position;
    }

    void Awake() {
        objectToFollow = GameObject.Find("Cylinder");
        initObjectPos = objectToFollow.transform.position;
    }

    // public void setObjectToFollow(GameObject obj) {

    //     obj = objectToFollow;
    //     initObjectPos = obj.transform.position;
    //     Debug.Log(initPos);
    //     Debug.Log(initObjectPos);
    // }

    void LateUpdate()
    {
        if (shouldUpdate) {
            var diff = objectToFollow.transform.position - initObjectPos;
            diff.y = 0;
            transform.position += diff;
            initObjectPos = objectToFollow.transform.position;
        }
    }
}
