using UnityEngine;
using System.Collections;

public class BackBallDetection : MonoBehaviour {


	private float move_speed;
	private Rigidbody carbody;
	public GameObject car;
	public Transform target;
	// Use this for initialization
	void Start () {
		carbody = car.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		move_speed = carbody.velocity.magnitude;  
		//Debug.Log("ball："+move_speed);  
		//改变小球的移动方向  
		transform.forward=target.position-transform.position;  
		transform.Translate(0,0,move_speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider go){
	//	Debug.Log ("name is "+go.transform.name);
		if (go.transform.name == "Car") {//表示后面有车追上来了
			if(transform.parent.GetChild(1).name == "Move_Turck"){
				transform.parent.GetChild(1).GetComponent<TruckControl>().SpeedUp();
			}else{
				transform.parent.GetChild(1).GetComponent<AutoCarControl>().SpeedUp();
			}
		}
	}
}
