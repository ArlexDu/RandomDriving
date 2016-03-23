using UnityEngine;
using System.Collections;

public class AutoCarControl : MonoBehaviour {

	public WheelCollider[] wheelcoilder = new WheelCollider[4];
	public GameObject[] wheel = new GameObject[4];
	public float maxmotor;
	private float motor;
	private float brakemotor = 0;
	private float maxangle = 25;
	public Transform target;
	private int maxspeed;
	private float handbrakemotor = 0;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0, -2, 0);
		motor = maxmotor;
		maxspeed = Random.Range (20, 30);
	}

	void FixedUpdate(){
		//用于更新当前前轮的状态  
		Vector3 offsetTargetPos= target.position;//小球的位置  
		Vector3 localTarget = transform.InverseTransformPoint(offsetTargetPos);  
		float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z)*Mathf.Rad2Deg;  
		float steer = Mathf.Clamp(targetAngle*0.05f, -1, 1)*Mathf.Sign(GetComponent<Rigidbody>().velocity.magnitude);  
		wheelcoilder[0].steerAngle=wheelcoilder[1].steerAngle=steer*maxangle;  
		if (handbrakemotor != 0) {
			motor = 0;
			brakemotor = handbrakemotor;
		} else {
			if (brakemotor != 0) {
				motor = 0;
			} else {
				motor = maxmotor;
			}
			float speed = GetComponent<Rigidbody> ().velocity.magnitude;
			//Debug.Log ("speed is "+ speed);
			if (speed > maxspeed) {
				motor = 0;
				brakemotor = 100;
				//GetComponent<Rigidbody> ().velocity=new Vector3(0,0,10*Time.deltaTime);
			} else {
				brakemotor = 0;
				motor = maxmotor;
			}
		}
		wheelcoilder[2].motorTorque=
			wheelcoilder[3].motorTorque=motor;  //车的动力
		wheelcoilder[2].brakeTorque = wheelcoilder[3].brakeTorque=brakemotor;//应用刹车动力
		//Debug.Log("go");
		changewheels ();
	}

	void changewheels()
	{
		for (int i=0;i<4;i++)
		{
			Quaternion quat;// it usual used to represent the rotation in unity.这个通常在unity中是表示旋转角度，包括物体在游戏世界的角度和自身的旋转角度
			Vector3 position;//unity中通常表示一个三维向量
			wheelcoilder[i].GetWorldPose(out position,out quat);//get the collides' position and ratation in the world.得到碰撞体的位置和状态
//			Debug.Log(i+" position is "+position);
			wheel[i].transform.position = position;//transform is position,roatation and scale of an object
//			Debug.Log(i+" wheel position is "+wheel[i].transform.position);
			wheel[i].transform.rotation = quat;
//			Debug.Log(i);//在控制栏中打印出相应的信息
		}
		
		
		
	}
	public void SlowDown(int bm){
		//Debug.Log ("Slow down");
		handbrakemotor = bm;
	}
	public void Normal(){
		//Debug.Log ("Normal");
		handbrakemotor = 0;
	}
	public void SpeedUp(){
		maxspeed += 10;
	}
}
