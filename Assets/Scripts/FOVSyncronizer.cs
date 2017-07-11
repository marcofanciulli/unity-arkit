using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVSyncronizer : MonoBehaviour {

    public Camera source;
    private Camera m_camera;

    private void Start()
    {
        m_camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        m_camera.fieldOfView = source.fieldOfView;
    }

}
