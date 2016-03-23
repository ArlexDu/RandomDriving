using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {
	private Transform goal;
	public GameObject car;
	public GameObject whole;
	private float move_speed;
	private Rigidbody carbody;
	private string way_name;
	// Use this for initialization
	void Start() {
		carbody = car.GetComponent<Rigidbody> ();
		//Debug.Log ("new truck");
		way_name = GameObject.Find ("Terrain1").GetComponent<AICarCreate> ().getpathname ();
		//find the way which the car will follow
		switch (way_name) {
		case "go1":
			goal = GameObject.Find("AIStop1").transform;
			break;
		case "go2":
		case "go3":
			goal = GameObject.Find("AIStop2").transform;
			break;
		case "go4":
			goal = GameObject.Find("AIStop4").transform;
			break;
		case "go5":
			goal = GameObject.Find("AIStop3").transform;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		move_speed = carbody.velocity.magnitude;  
		//Debug.Log("ball："+move_speed);  
		//改变小球的移动方向  
		transform.forward=goal.position-transform.position;  
		transform.Translate(0,0,move_speed*Time.deltaTime);
		//判断车辆是否掉到地形以下
		if(carbody.position.y<-10){
			GameObject.Find("Terrain1").GetComponent<AICarCreate>().reCreate(way_name);
			Destroy(whole);
		}
	}  
	void OnTriggerEnter(Collider go){  
		//碰撞检测，碰到前车的话要刹车
		if (go.transform.name == "Car") {
			if(transform.parent.GetChild(1).name == "Move_Turck"){
				transform.parent.GetChild(1).GetComponent<TruckControl>().SlowDown(800);
			}else{
				transform.parent.GetChild(1).GetComponent<AutoCarControl>().SlowDown(800);
			}
		} else if (go.transform.name == goal.name||go.transform.name == "End") {//到达终点,消灭车辆实例
			GameObject.Find("Terrain1").GetComponent<AICarCreate>().reCreate(way_name);
			Destroy(whole);
		}
	}  
	void OnTriggerExit(Collider go){
		//碰撞检测，碰到前车的话要刹车
		if (go.transform.name == "Car") {
			if (go.transform.name == "Car") {
				if(transform.parent.GetChild(1).name == "Move_Turck"){
					transform.parent.GetChild(1).GetComponent<TruckControl>().Normal();
				}else{
					transform.parent.GetChild(1).GetComponent<AutoCarControl>().Normal();
				}
		}

	}
}
}