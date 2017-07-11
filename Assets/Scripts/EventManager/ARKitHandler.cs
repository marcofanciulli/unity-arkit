using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARKitHandler : MonoBehaviour {

	public float scale;
	public GameObject target;
	// Use this for initialization

	void Start () {
		EventsManager.EventManager<EventEnum>.Instance.StartListening<ARHitEventInfo>(EventEnum.ARHit,SetObjectDinstance);

	}
	void SetObjectDinstance(ARHitEventInfo hitInfo){
		target.SetActive (true);
		gameObject.transform.position = hitInfo.position;
		gameObject.transform.rotation = hitInfo.rotation;
		float distance = (float)hitInfo.distance;
		ObjectDistance (distance);
	}
	void LateUpdate()
	{
		float distance = Vector3.Distance (gameObject.transform.position, Camera.main.transform.position);
		ObjectDistance (distance);
		
	}

	void ObjectDistance(float dist){
		Vector3 rateOne = (transform.position - Camera.main.transform.position);
		Vector3 rate = new Vector3(rateOne.x ,rateOne.y ,rateOne.z ) / scale + Camera.main.transform.position;
		target.transform.position = rate;
	}

	void OnDestroy() {
		EventsManager.EventManager<EventEnum>.Instance.StopListening<ARHitEventInfo> (EventEnum.ARHit, SetObjectDinstance);
	}


}
