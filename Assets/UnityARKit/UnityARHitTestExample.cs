using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS
{
	public class UnityARHitTestExample : MonoBehaviour
	{
		public Transform m_HitTransform;
		public GameObject gameObject;
		[Header("ARHitTestResultType")]
		public bool TypeExistingPlaneUsingExtent;
		public bool TypeExistingPlane;
		public bool TypeFeaturePoint;
		public bool TypeVerticalPlane;
		public bool TypeHorizontalPlane;

		private float JustForTest;
        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");

                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);

					JustForTest = GetObjectDinstanse (hitResult, gameObject);

					Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######} dist:{3:0.######} newDist:{4:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z,hitResult.distance, JustForTest));
                    return true;
                }
            }
            return false;
        }

		float GetObjectDinstanse (ARHitTestResult hitRes,GameObject obj)
		{
			float HitTestDistanse;
			float ObjectYScale = obj.transform.localScale.y;
			double distance = hitRes.distance;
			double ratio,result,BaseTriangle,HitBaseTriangle;
			double FieldOfView = Camera.main.fieldOfView;
			double radians = FieldOfView * (Math.PI/180);

			BaseTriangle = 2*distance*Math.Tan(radians/2);
			ratio = BaseTriangle / ObjectYScale;
			HitBaseTriangle = Convert.ToSingle(ObjectYScale) * ratio;
			result = (HitBaseTriangle / (2 * Math.Tan (radians / 2)));

			Debug.Log (string.Format ("BaseTriangle:{0:0.######} ratio:{1:0.######} HitBaseTriangle:{2:0.######} result:{3:0.######}", BaseTriangle, ratio, HitBaseTriangle, result));

			HitTestDistanse = Convert.ToSingle (result);

			return HitTestDistanse;
		}

		void Setup () {
			

		}

		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 && m_HitTransform != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

					List<ARHitTestResultType> typesList = new List<ARHitTestResultType>();
					if (TypeExistingPlaneUsingExtent)typesList.Add (ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
					if (TypeExistingPlane)typesList.Add (ARHitTestResultType.ARHitTestResultTypeExistingPlane);
					if (TypeFeaturePoint)typesList.Add (ARHitTestResultType.ARHitTestResultTypeFeaturePoint);
					if (TypeVerticalPlane)typesList.Add (ARHitTestResultType.ARHitTestResultTypeVerticalPlane);
					if (TypeHorizontalPlane)typesList.Add (ARHitTestResultType.ARHitTestResultTypeHorizontalPlane);
                    // prioritize reults types
					ARHitTestResultType[] resultTypes = typesList.ToArray();



						

                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                            return;
                        }
                    }

				}
			}
		}

	
	}
}

