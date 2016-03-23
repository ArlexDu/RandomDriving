using UnityEngine;
using System.Collections;

public class TruckControl : MonoBehaviour {

	public Transform target;
	public WheelCollider[] wheelcoilder= new WheelCollider[14];
	//实际的轮子的声明
	public GameObject[] wheel= new GameObject[14];
	private float Maxmotor = 10000; //最大的动力
	private float Maxangle = 25; //最大的旋转角度
	private float BrokeTorque; //最大的刹车动力
	private float maxSpeed;
	// Use this for initialization
	
	void Start () {//初始化函数

		//Debug.Log(GetComponent<Rigidbody>().centerOfMass);//获得当前物体的刚体组件并且获得它的重心
		transform.FindChild("Truck_Cab").GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0, -3, 0);
		transform.FindChild("Trailer").GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0, -3, 0);
		maxSpeed = Random.Range (20, 30);
	}
	
	// Update is called once per frame
	
	void FixedUpdate () {//每一帧的调用函数
		
		Vector3 offsetTargetPos= target.position;//小球的位置  
		//Debug.Log ("offsetTargetPos is "+offsetTargetPos);
		Vector3 localTarget =transform.FindChild("Truck_Cab").transform.InverseTransformPoint(offsetTargetPos);
		//Debug.Log ("localTarget is "+localTarget);
		float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z)*Mathf.Rad2Deg; 
		//Debug.Log ("tragetAngle is "+targetAngle);
		float steer = Mathf.Clamp(targetAngle*0.05f, -1, 1)*Mathf.Sign( transform.FindChild("Truck_Cab").GetComponent<Rigidbody>().velocity.magnitude);  
		wheelcoilder[0].steerAngle=wheelcoilder[1].steerAngle=steer*Maxangle; 
		//Debug.Log ("steer is "+steer*Maxangle);
		if (BrokeTorque != 0) {
			Maxmotor = 0;
		} else {
			Maxmotor = 10000;
		}
		float speed = transform.FindChild("Truck_Cab").GetComponent<Rigidbody> ().velocity.magnitude;
		if (speed > maxSpeed) {
			Maxmotor = 0;
			BrokeTorque = 100;
			//GetComponent<Rigidbody> ().velocity=new Vector3(0,0,10*Time.deltaTime);
		} else {
			BrokeTorque = 0;
			Maxmotor = 10000;
		}
		//Debug.Log ("speed is "+ speed);
		wheelcoilder[2].motorTorque =
		wheelcoilder[3].motorTorque = wheelcoilder[4].motorTorque =wheelcoilder[5].motorTorque =
	    wheelcoilder[10].motorTorque = wheelcoilder[11].motorTorque =
		wheelcoilder[12].motorTorque = wheelcoilder[13].motorTorque =Maxmotor;  //车的动力

		wheelcoilder[2].brakeTorque = wheelcoilder[3].brakeTorque = wheelcoilder[4].brakeTorque = 
		wheelcoilder[10].brakeTorque = wheelcoilder[11].brakeTorque = wheelcoilder[12].brakeTorque = wheelcoilder[13].brakeTorque =
		wheelcoilder[5].brakeTorque=BrokeTorque;//应用刹车动力

		changewheels();
		AddDownForce ();
	}
	
	void changewheels()
	{
		for (int i=0;i<14;i++)
		{
			Quaternion quat;// it usual used to represent the rotation in unity.这个通常在unity中是表示旋转角度，包括物体在游戏世界的角度和自身的旋转角度
			Vector3 position;//unity中通常表示一个三维向量

			wheelcoilder[i].GetWorldPose(out position,out quat);//get the collides' position and ratation in the world.得到碰撞体的位置和状态
			wheel[i].transform.position = position;//transform is position,roatation and scale of an object
			wheelcoilder[9].GetWorldPose(out position,out quat);
			wheel[i].transform.rotation = quat;
			//Debug.Log(i);//在控制栏中打印出相应的信息
		}

    }
	public void SlowDown(int bm){
		//Debug.Log ("Slow down");
		BrokeTorque = bm;
	}
	public void Normal(){
		//Debug.Log ("Normal");
		BrokeTorque = 0;
	}
	// this is used to add more grip in relation to speed
	private void AddDownForce()
	{
		wheelcoilder[0].attachedRigidbody.AddForce(-transform.up*100*
		                                           wheelcoilder[0].attachedRigidbody.velocity.magnitude);
	}
	public void SpeedUp(){
		maxSpeed += 10;
	}
}
