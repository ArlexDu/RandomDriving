using UnityEngine;
using System.Collections;

public class newcarposition : MonoBehaviour {

	private GameObject suv;
	// Use this for initialization
	void Start () {
		suv = GameObject.Find("SUV");
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x ,transform.position.y,suv.transform.position.z);
	}
}
