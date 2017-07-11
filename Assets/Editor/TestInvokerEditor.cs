#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestInvoker))]
public class TestInvokerEditor : Editor {

	public override void OnInspectorGUI()
	{

		DrawDefaultInspector();

		var myTarget = (TestInvoker)target;

		if(GUILayout.Button("ARClick"))
		{
			myTarget.PerformARClick ();
		}
	}
}



#endif