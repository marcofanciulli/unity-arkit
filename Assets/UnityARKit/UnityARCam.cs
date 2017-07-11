using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS
{
		public class UnityARCam : MonoBehaviour {

					private ARCamera attaCam;
					public Vector3 translation = new Vector3(0,0,-50);
					public Vector3 eulerAngles;
					public Vector3 scale = new Vector3(1, 1, 1);

					void Start () 
					{

						Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
						attaCam = GetComponent<ARCamera> ();
						attaCam.worldTransform.SetTRS(translation, rotation, scale);
									
					}
									
									// Update is called once per frame
					void Update () {
										
					}
		}
}