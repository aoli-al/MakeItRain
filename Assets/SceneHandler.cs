using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using DecalSystem;

public class SceneHandler : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private bool shouldDraw = false;
    private Transform previousTransform;
    public GameObject blood;
    public GameObject bloodStatic;
    public GameObject objectToFollow;
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
        // plane.transform.position = hit.transform.position;
        // plane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
