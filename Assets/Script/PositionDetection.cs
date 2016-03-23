using UnityEngine;
using System.Collections;

public class PositionDetection : MonoBehaviour {
	// Use this for initialization
	private string name;
	void Start () {
		name = transform.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider go){
	//	Debug.Log ("name is "+go.transform.name);
		if (name == "2") {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionTwo (false);
		} else {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionThree (false);
		}
	}
	void OnTriggerStay(Collider go){
		if (name == "2") {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionTwo (false);
		} else {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionThree (false);
		}
	}
	void OnTriggerExit(Collider go){
	//	Debug.Log ("name is "+go.transform.name);
		if (name == "2") {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionTwo (true);
		} else {
			GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().JudgeCreatePositionThree (true);
		}
	}

}
