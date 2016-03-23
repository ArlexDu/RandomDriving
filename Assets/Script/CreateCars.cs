using UnityEngine;
using System.Collections;

public class CreateCars : MonoBehaviour {

    public GameObject[] car = new GameObject[3];//四种车型
    public Transform[] startPositions = new Transform[4];//五个起始位置
    static public GameObject[] newCar = new GameObject[5];//五个起始位置对应的五辆车
                                                                                                    //i = 1和2对应最右侧的两辆车，1代表前车，2代表后车
    public GameObject m_Car;

    GameObject frontCar;
    GameObject backCar;

    float mCarZ;

    float frontCarZ;
    float backCarZ;

    float distanceFront;
    float distanceBack;

    void Start() {

    }

    void FixedUpdate() {
        for (int i = 0; i < 4; ++i) {
            if (newCar[i] == null) {
                if (i <= 2) {
                    newCar[i] = (GameObject)Instantiate(car[Random.Range(0, 3)], startPositions[i].position, new Quaternion(0, 0, 0, 0));
                    if (i == 1) AllowRightManager.front_car = frontCar = newCar[1];
                    if (i == 2) AllowRightManager.back_car = backCar = newCar[2];
                }
                else
                    newCar[i] = (GameObject)Instantiate(car[Random.Range(0, 3)], startPositions[i].position, new Quaternion(0, 1, 0, 0));
                car_init(newCar[i]);
            }
        }
        mCarZ = m_Car.transform.position.z;
        frontCarZ = frontCar.transform.GetChild(1).position.z;
        backCarZ = backCar.transform.GetChild(1).position.z;
        distanceBack = frontCarZ - mCarZ;
        distanceFront = backCarZ - mCarZ;
    }

    void car_init(GameObject car) {
        car.SetActive(true);
    }

    public float GetDistanceFront() {
        return distanceFront;
    }

    public float GetDistanceBack() {
        return distanceBack;
    }
}
