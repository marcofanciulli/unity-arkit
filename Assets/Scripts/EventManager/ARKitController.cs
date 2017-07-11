using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARKitController : MonoBehaviour{

	public bool hitBoundedPlanes = true;
	public bool hitInfinitePlanes = false;
	public bool hitPotentialPlanes = true;
	public bool hitFeaturePoints = true;

	[SerializeField]
	private GameObject planePrefab;

	[SerializeField]
	private Material arPlaneIndicatorMaterial;

	[SerializeField]
	private float planeIndicatorSonarDistance = 7.0f;
	[SerializeField]
	private float planeIndicatorSonarSpeed = 4.0f;
	private float currentIndicatorSonarDistance = 0.0f;

	void Update () {
		if (Input.touchCount > 0){
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began){
				
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};

				if (hitBoundedPlanes && HitTestWithResultType (point, ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent))
					return;
				
				if(hitInfinitePlanes && HitTestWithResultType (point, ARHitTestResultType.ARHitTestResultTypeExistingPlane))
					return;
					
				if(hitPotentialPlanes && HitTestWithResultType (point, ARHitTestResultType.ARHitTestResultTypeHorizontalPlane))
					return;
					
				if(hitFeaturePoints && HitTestWithResultType (point, ARHitTestResultType.ARHitTestResultTypeFeaturePoint))
					return;

			}
		}
		if (arPlaneIndicatorMaterial != null) {
			var cameraPos = Camera.main.transform.position;
			arPlaneIndicatorMaterial.SetVector ("_DistAnchor", cameraPos);
			arPlaneIndicatorMaterial.SetFloat ("_DistPoint", currentIndicatorSonarDistance);
			float delta = planeIndicatorSonarSpeed * Time.deltaTime;
			currentIndicatorSonarDistance = (currentIndicatorSonarDistance + delta) % planeIndicatorSonarDistance;
		}
	}

	public void Start () {
		eventManager = EventsManager.EventManager<EventEnum>.Instance;
		unityARAnchorManager = new UnityARAnchorManager();
		UnityARUtility.InitializePlanePrefab (planePrefab);

	}

	private void OnDestroy () {
		if (unityARAnchorManager != null) {
			unityARAnchorManager.Destroy ();
			UnityARSessionNativeInterface.GetARSessionNativeInterface ().Pause ();
		}

	}

	void OnGUI()
	{
		List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
		if (arpags.Count >= 1) {
			//ARPlaneAnchor ap = arpags [0].planeAnchor;
			//GUI.Box (new Rect (100, 100, 800, 60), string.Format ("Center: x:{0}, y:{1}, z:{2}", ap.center.x, ap.center.y, ap.center.z));
			//GUI.Box(new Rect(100, 200, 800, 60), string.Format ("Extent: x:{0}, y:{1}, z:{2}", ap.extent.x, ap.extent.y, ap.extent.z));
		}
	}

	private bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultType){
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultType);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
				Vector3 pointPosition = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
				Quaternion pointRotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
				ARPlaneAnchorGameObject anchor = getAnchor (hitResult.anchorIdentifier);
				float distance = (float)hitResult.distance;
				Debug.Log (string.Format ("distance:{0:0.######}", distance));
				ARHitEventInfo hitEventInfo = new ARHitEventInfo {
					position = pointPosition,
					rotation = pointRotation,
					plane = anchor,
					distance = distance
				};
				unityARAnchorManager.Destroy ();
				eventManager.TriggerEvent<ARHitEventInfo> (EventEnum.ARHit, hitEventInfo);
				return true;
			}
		}
		return false;
	}

	private ARPlaneAnchorGameObject getAnchor(string anchorId){
		var anchors = unityARAnchorManager.GetCurrentPlaneAnchors ();
		foreach (var anchor in anchors) {
			if (anchor.planeAnchor.identifier == anchorId) {
				return anchor;
			}
		}
		return null;
	}

	private EventsManager.EventManager<EventEnum> eventManager = null;
	private UnityARAnchorManager unityARAnchorManager = null;

	}