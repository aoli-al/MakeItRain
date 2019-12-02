using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using DecalSystem;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System;



public class SceneHandler : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private bool shouldDraw = false;
    private Transform previousTransform;
    public GameObject blood;
    public GameObject bloodStatic;
    public GameObject objectToFollow;
    public GameObject bloodRipple;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;
    public GameObject Text6;
    public GameObject Text7;
    public GameObject Text8;

    public GameObject CameraFloor;
    public GameObject CameraBack;
    int triggerCount = 0;

    bool Calibration = false;

    private List<Vector3> PosList = new List<Vector3>();


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
        if (PosList.Count < 4) {
            PosList.Add(objectToFollow.transform.position);
        } else if (! Calibration) {
            Calibration = true;
            var nx = (PosList[0].x + PosList[2].x) / 2;
            var ny = (PosList[0].z + PosList[2].z) / 2;
            CameraFloor.transform.position = new Vector3(nx, 7.5f, ny);
            var dx = (PosList[1].x - PosList[0].x);
            var dz = (PosList[1].z - PosList[0].z);
            var angle = Math.Atan(dz/dx) * (180/Math.PI);
            CameraFloor.transform.rotation = Quaternion.Euler(new Vector3(90, -90, (float) angle));
        }

        triggerCount += 1;


        // TRIGGER WILL ENABLE TEXT
        if (triggerCount == 1) {
            Text1.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 3) {
            Text2.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 5) {
            Text3.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 7) {
            Text4.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 8) {
            Text4.GetComponent<Renderer>().enabled = false;
            Text5.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 10) {
            Text6.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 12) {
            Text7.GetComponent<Renderer>().enabled = true;
        } else if (triggerCount == 14) {
            Text8.GetComponent<Renderer>().enabled = true;
        }

        // TRIGGER WILL ENABLE GUNSHOT
        else {
                Text1.GetComponent<Renderer>().enabled = false;
                Text2.GetComponent<Renderer>().enabled = false;
                Text3.GetComponent<Renderer>().enabled = false;
                Text4.GetComponent<Renderer>().enabled = false;
                Text5.GetComponent<Renderer>().enabled = false;
                Text6.GetComponent<Renderer>().enabled = false;
                Text7.GetComponent<Renderer>().enabled = false;
                Text8.GetComponent<Renderer>().enabled = false;

            Debug.Log("Button was clicked");
            previousTransform = e.target;
            shouldDraw = true;
            int outHostId;
            int outConnectionId;
            int outChannelId;
            byte[] buffer = new byte[1024];
            int bufferSize = 1024;
            int receiveSize;
            byte error;

            var hits = Physics.RaycastAll(transform.position, transform.forward);
            foreach (var hit in hits) {
                if (new List<string>() {"Cylinder", "Back"}.Contains(hit.collider.gameObject.name)) {
                    if (hit.collider.gameObject.name == "Cylinder")
                    {
                        var a = Instantiate(blood, hit.point, Quaternion.Euler(0, 0, -90));
                        DecalSystem.DecalBuilder.Build(a.GetComponent<Decal>());
                        Debug.Log(objectToFollow);
                        a.GetComponent<UpdateBloodDecayPos>().shouldUpdate = true;
                    } else {
                        var a = Instantiate(bloodStatic, hit.point, Quaternion.Euler(0, 0, -90));
                    }
                }
            }
            var p = bloodRipple.GetComponent<ParticleSystem>();
            p.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, 5, 10, 0, 0.1f));
            var endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.237"), 2001);
            var client = new UdpClient();
            client.Send(Encoding.UTF8.GetBytes("0"), 1, endPoint);
            // plane.transform.position = hit.transform.position;
            // plane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
    }

    private void BuildDecal(Decal decal)
    {
    }

    private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers)
    {
        MeshRenderer[] renderers = (MeshRenderer[])GameObject.FindObjectsOfType<MeshRenderer>();
        List<GameObject> objects = new List<GameObject>();
        foreach (Renderer r in renderers)
        {
            if (!r.enabled) continue;
            // if (!IsLayerContains(affectedLayers, r.gameObject.layer)) continue;
            if (r.GetComponent<Decal>() != null) continue;

            if (bounds.Intersects(r.bounds))
            {
                objects.Add(r.gameObject);
            }
        }
        return objects.ToArray();
    }
}
