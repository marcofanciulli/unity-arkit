using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public struct ARHitEventInfo {

	public Vector3 position;
	public Quaternion rotation;
	public ARPlaneAnchorGameObject plane;
	public float distance;

}
