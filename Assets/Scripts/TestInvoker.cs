using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvoker : MonoBehaviour {

	public Vector3 pos;


	public void PerformARClick(){
		ARHitEventInfo info = new ARHitEventInfo {
			position = pos,
			rotation = transform.rotation,
			distance = Vector3.Distance(Camera.main.transform.position,pos)
		};
		EventsManager.EventManager<EventEnum>.Instance.TriggerEvent<ARHitEventInfo>(EventEnum.ARHit,info);

	}

}
