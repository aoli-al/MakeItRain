using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject decalPrefab1 = null;
    public GameObject decalPrefab2 = null;
    public GameObject decalPrefab3 = null;
    public GameObject decalPrefab4 = null;
    public GameObject decalPrefab5 = null;
    public GameObject decalPrefab6 = null;
    public GameObject decalPrefab7 = null;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        prefabList.Add(decalPrefab1);
        prefabList.Add(decalPrefab2);
        prefabList.Add(decalPrefab3);
        prefabList.Add(decalPrefab4);
        prefabList.Add(decalPrefab5);
        prefabList.Add(decalPrefab6);
        prefabList.Add(decalPrefab7);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") == true) {
            //get position of mouseclick in scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) == true) {
                SpawnDecal(hit);
            }

        }
        
    }

    private void SpawnDecal(RaycastHit hit) {
        int preFabIndex = UnityEngine.Random.Range(0, prefabList.Count - 1);
        GameObject decal = Instantiate(prefabList[preFabIndex]);
        // 1. face decal same as surface
        //decal.transform.forward = hit.normal * -1f;

        // 2. position on surface
        decal.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

        //3. just above surface (so doesn't conflict for visibility)
        decal.transform.Translate(Vector3.forward * -0.01f);
    }

    private void OnDrawGizmos() {
        // visualize the normal of the decal in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, 0.2f);
        Gizmos.DrawLine(hit.point, hit.point + hit.normal);
        Gizmos.DrawSphere(hit.point + hit.normal, 0.0f);
    }

}
