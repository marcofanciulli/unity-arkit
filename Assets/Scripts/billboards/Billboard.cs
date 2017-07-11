using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	
	void LateUpdate () {
        Quaternion rotation = Camera.main.transform.rotation;
        transform.rotation = rotation * Quaternion.AngleAxis(180.0f, Vector3.up);
    }

}

