using UnityEngine;
using System.Collections;

public class Destory_Car : MonoBehaviour {

    public GameObject wholeCar;

	void Start () {
	    
	}
	
	void FixedUpdate () {
	    if(transform.position.y < -5) {
            Destroy(wholeCar);
        }
	}

    void OnTriggerEnter(Collider other) {
        if(other.transform.name == "AIStop1"||
            other.transform.name == "AIStop2"||
            other.transform.name == "AIStop3" ||
            other.transform.name == "AIStop4")
        Destroy(wholeCar);
    }
}
