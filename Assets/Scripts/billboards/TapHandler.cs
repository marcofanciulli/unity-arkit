using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHandler : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo = new RaycastHit();
            var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                var activator = hitInfo.transform.gameObject.GetComponent<BillboardActivator>();
                if(activator != null) {
                    activator.ToggleTarget();
                }
            }
        }
	}
}
