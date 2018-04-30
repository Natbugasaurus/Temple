using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour {
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 1.5f, Color.red);

        if ((Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 1.5f))) {       
            if (hit.collider.gameObject.name == "Urn") {
                
            }
        }
    }
}
