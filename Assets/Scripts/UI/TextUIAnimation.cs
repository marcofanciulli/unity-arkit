using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIAnimation : MonoBehaviour {

	public GameObject phone;

	void Start(){
		
	}

	public void onCompilte2DAnim(){
		phone.SetActive (true);
	}
	public void onComplite3DAnim(){
		phone.SetActive (false);
	}

}