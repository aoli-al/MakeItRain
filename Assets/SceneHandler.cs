using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private bool shouldDraw = false;
    private Transform previousTransform;
    public GameObject blood;
    // Start is called before the first frame update

    void Start()
    {
        laserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log("Button was clicked");
        previousTransform = e.target;
        shouldDraw = true;
        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);
        Debug.Log(hit.transform.position);
        if (bHit) {
            Instantiate(blood, hit.point, Quaternion.Euler(0, 0, -90));
        }
        // plane.transform.position = hit.transform.position;
        // plane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log("Cube was entered");
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log("Cube was exited");
    }

}
