using UnityEngine;
using System.Collections;

public class AllowRightManager : MonoBehaviour {

    public float distanceOfFront = 50.0f;
    public float distanceOfBack = 80.0f;

    private GameObject allowThrough;//地上显示绿色的GameObject
    private Renderer greenRenderer;
    private GameObject noThrough;//地上显示红色的GameObject
    private Renderer redRenderer;

    private GameObject SUVBan;//禁止右转标志的GameObject

    private Renderer rendererAllow;//允许右转的标志
    private Renderer rendererBan;//禁止右转的标志

    private GameObject suv;
    static public GameObject front_car;//前车
    static public GameObject back_car;//后车
    static public bool creatPick = false;
    static public bool creatTruck = false;

    static bool wantThrough = false;
    static bool isAllowed = false;//显示是否允许换道

    GameObject[] newCar = new GameObject[5];

    void Start () {
        allowThrough = GameObject.Find("AllowThrough");
        greenRenderer = allowThrough.GetComponent<Renderer>();

        noThrough = GameObject.Find("NoThrough");
        redRenderer = noThrough.GetComponent<Renderer>();

        SUVBan = GameObject.Find("Car/SUV/Ban");
        rendererBan = SUVBan.GetComponent<Renderer>();

        rendererAllow = GetComponent<Renderer>();

        suv = GameObject.Find("Car/SUV");
        //pick = GameObject.Find("MovePick" + "(Clone)");
        //truck = GameObject.Find("MovePick" + "(Clone)");
	}
	
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.Keypad0))//想换道，请按0
            wantThrough = true;
        if (Input.GetKey(KeyCode.Keypad1))//取消换道，请按1
            wantThrough = false;

		//float right = Input.GetAxis ("Right");

		//if (right == 1) {
		//	wantThrough = true;
		//} else {
		//	wantThrough = false;
		//}


		//Debug.Log (right);

        if (wantThrough){//如果想换道
            if (isAllowed){//如果允许换道
                showGreen();
                closeRed();
                //Debug.Log("showGreen");
            }
            else{//不允许换道
                showRed();
                closeGreen();
                //Debug.Log("showRed");
            }
        }
        else{
            closeGreen();
            closeRed();
        }
        isAllowed = checkThrough();
    }

    bool checkThrough()//检测能否变道
    {
        float suvZ = suv.transform.position.z;
        float front_car_z = -100000.0f;
        float back_car_z = -1000000.0f;
        float suvVz = suv.transform.GetComponent<Rigidbody>().velocity.z;
        float front_car_Vz = front_car.transform.GetChild(1).GetComponent<Rigidbody>().velocity.z;
        float back_car_Vz = back_car.transform.GetChild(1).GetComponent<Rigidbody>().velocity.z;
        bool front_car_Ok = false;
        bool back_car_Ok = false;
        //Debug.Log("suvVz = " + suvVz);
        //Debug.Log("pickVz = " + pickVz);
        //Debug.Log("truckVz = " + truckVz);
        if (front_car != null)
            front_car_z = front_car.transform.GetChild(1).position.z;
        if(back_car != null)
            back_car_z = back_car.transform.GetChild(1).position.z;

        if (suvZ - front_car_z > distanceOfBack || front_car_z - suvZ > distanceOfFront)
            front_car_Ok = true;
        else {
            if (front_car_z - suvZ > 10) {
                if (front_car_Vz > suvVz)
                    front_car_Ok = true;
                else
                    front_car_Ok = false;
            }
            else {
                if (front_car_Vz < suvVz)
                    front_car_Ok = true;
                else
                    front_car_Ok = false;
            }
        }

        if (suvZ - back_car_z > distanceOfBack || back_car_z - suvZ > distanceOfFront)
            back_car_Ok = true;
        else {
            if (back_car_z - suvZ > 10) {
                if (back_car_Vz > suvVz)
                    back_car_Ok = true;
                else
                    back_car_Ok = false;
            }
            else {
                if (back_car_Vz < suvVz)
                    back_car_Ok = true;
                else
                    back_car_Ok = false;
            }
        }
        return back_car_Ok && front_car_Ok;
    }

    bool showGreen()
    {
        greenRenderer.enabled = true;
        rendererAllow.enabled = true;
        return true;
    }

    bool showRed()
    {
        redRenderer.enabled = true;
        rendererBan.enabled = true;
        return true;
    }

    bool closeGreen()
    {
        greenRenderer.enabled = false;
        rendererAllow.enabled = false;
        return true;
    }

    bool closeRed()
    {
        redRenderer.enabled = false;
        rendererBan.enabled = false;
        return true;
    }

    private int RoadNumber(GameObject other)
    {
        if (other != null)
        {
            if (other.transform.position.x >= 153.3f && other.transform.position.x < 165.7f)
                return 1;
            if (other.transform.position.x >= 165.7f && other.transform.position.x < 179.0f)
                return 2;
            if (other.transform.position.x >= 179.0f && other.transform.position.x < 192.5f)
                return 3;
            if (other.transform.position.x >= 192.5f && other.transform.position.x < 205.0f)
                return 4;
        }
        return 0;
    }
}