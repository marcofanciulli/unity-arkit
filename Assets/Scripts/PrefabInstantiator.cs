using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour {

	public GameObject prefab;

	void Start () {
		if (prefab != null) {
			Instantiate (prefab, transform);
		}
	}

}
