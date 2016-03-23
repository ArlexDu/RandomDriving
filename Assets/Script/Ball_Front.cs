using UnityEngine;
using System.Collections;

public class Ball_Front : MonoBehaviour {

    public GameObject car;
    public GameObject wholeCar;
    private Rigidbody carRigidbody;
    public float Front_Distance;

    void Start() {
        carRigidbody = car.GetComponent<Rigidbody>();
        Front_Distance = 50;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(transform.position.z > car.transform.position.z)
            transform.position = new Vector3(transform.position.x, transform.position.y, car.transform.position.z + Front_Distance);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, car.transform.position.z - Front_Distance);
    }

    void OnTriggerEnter(Collider go) {//碰撞检测，碰到前车的话要刹车
        if (go.transform.name == "Car") {
            transform.parent.GetChild(1).GetComponent<AutoCarControl>().SlowDown(800);
        }
    }
    void OnTriggerExit(Collider go) {//碰撞检测，碰到前车的话要刹车
        if (go.transform.name == "Car") {
            transform.parent.GetChild(1).GetComponent<AutoCarControl>().Normal();
        }
    }
}
