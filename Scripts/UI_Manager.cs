using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Transform playerROV;    //잠수정
    public Transform camera;    //잠수함 따라 다니는 카메라
    public Transform seaPos;    //바다 표면 시작 위치
    public GameObject[] battery;    //밧데리
    public GameObject[] viewBattery;    //뷰밧데리
    public FPCSwimmer.FPCSwimmer_3 playerSpeed;
    public GameObject leapMotionCtrl;   //일시정지를 위한 립모션 컨트롤러
    public Transform leftHand_Obj1, leftHand_Obj2, leftHand_Obj3, rightHand_Obj1, rightHand_Obj2, rightHand_Obj3;
    public Toggle simulationToggle; //시뮬레이션뷰토글
    public Toggle realToggle;   //리얼뷰토글
    public GameObject simulationViewOut;    //시뮬레이션뷰 화면 안보이게하는 뷰
    public GameObject realViewOut;  //리얼뷰 화면 안보이게하는 뷰
    public GameObject[] lockImg;    //바다속에서 속정 시켰는지 포즈



    float seaDistance;  //바다 깊이
    float batteryTime;  //밧데리 소모시간 현재
    float batteryStartTime = 60f;    //밧데리 소모시간
    Vector3 startPos;   //처음 위치(과거 위치)
    float disSub, disTotal; //이동거리, 총이동거리
    float speedStringChage; //숫자로 변환
    bool pauseState; //일시정지
    bool _lock; //현재 위치 고정  
    Vector3 rov_Pos, camera_Pos;


    [Header("Text")]
    public Text depthText;  //깊이
    public Text batteryText;    //밧데리 텍스트
    public Text distanceText;   //거리 텍스트
    public Text speed;  //속도

    public Transform stopPos;
    public Transform ropePos;
    bool stopState, ropeState;


    

    void Start()
    {
        batteryTime = batteryStartTime; //밧데리 소모 시간 초기화
        startPos = playerROV.transform.position; //로봇 처음 위치 저장
        simulationViewOut.SetActive(false); //시뮬레이터(화면 안보임) 뷰 비활성
        leapMotionCtrl.SetActive(false);    //립모션 컨트롤로 비활성화(움직이는 동안은 팔이 움직이면 안된다.)
        lockImg[0].SetActive(true); lockImg[1].SetActive(false);    //물속에서 고정시켰는지 이미지로 초기화


        StartCoroutine(BatteryUse());   //밧데리함수

        //meshFilter = GameObject GetComponent<MeshFilter>();
    }

  

    
    void Update()
    {
        if (stopState)
        {
            playerROV.position = stopPos.position;
        }

        if(ropeState)
        {
            playerROV.position = ropePos.position;
        }


        if (_lock)
        {
            lockImg[0].SetActive(false); lockImg[1].SetActive(true);
            playerROV.localPosition = rov_Pos;
            //if (pauseState)
            //    leapMotionCtrl.SetActive(false);
           // else
                leapMotionCtrl.SetActive(true);
            //Debug.Log("rov_Pos" + rov_Pos + " playerROV " + playerROV.localPosition);
        }
        else
        {
            lockImg[0].SetActive(true); lockImg[1].SetActive(false);
            if (pauseState)
                leapMotionCtrl.SetActive(false);
            else
                leapMotionCtrl.SetActive(true);
            //StandbyMode();  //초기화
        }

        SeaDeepValue(); //뭎속 깊이 측정 함수
        TravelRange();  //이동거리 함수
        ROV_Speed();    //속도 함수

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //팔 일시정지
            RobotHandPause();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            //초기화
            StandbyMode();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            //고정위치(물 속에서 포즈) 및 손 움직이게 
            PosLock();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StopPos();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            RopeStopPos();
        }
    }


    void StopPos()
    {
        if (!stopState)
        {
            stopState = true;
            Debug.Log("들어옴");
        }
        else
        {
            stopState = false;
        }
    }

    void RopeStopPos()
    {
        if (!ropeState)
        {
            ropeState = true;
            Debug.Log("들어옴");
        }
        else
        {
            ropeState = false;
        }
    }

    //물속 깊이 측정 함수
    void SeaDeepValue()
    {
        seaDistance = seaPos.position.y - playerROV.position.y;

        if(seaPos.position.y > playerROV.position.y)
        {
            depthText.text = seaDistance.ToString("N0");
        }
    }


    //밧데리
    IEnumerator BatteryUse()
    {
        while (batteryTime > 0)
        {
            batteryTime -= Time.deltaTime;
            //Debug.Log(batteryTime);
            yield return new WaitForEndOfFrame();
        }
        batteryTime = 0f;
        if (battery[0].activeSelf == true)
        {
            battery[0].SetActive(false);
            viewBattery[0].SetActive(false);
            batteryText.text = "80%";
            batteryTime = batteryStartTime;
        }
        else if (battery[1].activeSelf == true)
        {
            battery[1].SetActive(false);
            viewBattery[1].SetActive(false);
            batteryText.text = "60%";
            batteryTime = batteryStartTime;
        }
        else if (battery[2].activeSelf == true)
        {
            battery[2].SetActive(false);
            viewBattery[2].SetActive(false);
            batteryText.text = "40%";
            batteryTime = batteryStartTime;
        }
        else if (battery[3].activeSelf == true)
        {
            battery[3].SetActive(false);
            viewBattery[3].SetActive(false);
            batteryText.text = "20%";
            batteryTime = batteryStartTime;
        }
        else if (battery[4].activeSelf == true)
        {
            battery[4].SetActive(false);
            viewBattery[4].SetActive(false);
            batteryText.text = "0%";
            batteryTime = batteryStartTime;
        }
        if (battery[4].activeSelf != false)
            StartCoroutine(BatteryUse());
    }


    //이동거리 측정
    void TravelRange()
    {
        //처음위치와 현재 위치가 다를경우(움직였다는 조건)
        if (startPos != playerROV.transform.position)
        {
            //현재 움직인 거리와 과거 위치 거리를 측정
            disSub = Vector3.Distance(startPos, playerROV.transform.position);

            disTotal += disSub; //거리총 값에 위에서 구한 거리를 더해줌
            //Debug.Log(disTotal);
            distanceText.text = (Mathf.FloorToInt(disTotal * 0.1f)).ToString();
            //Debug.Log(Mathf.FloorToInt(disTotal));
            startPos = playerROV.transform.position; //현재 위치를 처음위치 변수값에 넣어줌.
        }
    }


    //ROV 속도 측정 함수
    void ROV_Speed()
    {
        speedStringChage = float.Parse(playerSpeed.speed_copy.ToString("N1"));

        speed.text = (speedStringChage * 10f).ToString();
        
    }

    //일시정지 함수
    public void RobotHandPause()
    {
        if (!pauseState)
        {
            pauseState = true;
        }
        else
        {
            pauseState = false;
        }

        if (pauseState)
        {
            leapMotionCtrl.SetActive(false);    //모션 인식을 하지 못하기 
            Time.timeScale = 0; //일시정지
        }
        else
        {
            Time.timeScale = 1; //일시정지 해제
            leapMotionCtrl.SetActive(true);
            StandbyMode();
            
        }
    }

    //초기화모드 함수
    public void StandbyMode()
    {
        leftHand_Obj1.localRotation = Quaternion.Euler(0, 0, 0);
        leftHand_Obj2.localRotation = Quaternion.Euler(-180f, 0, 90f);
        leftHand_Obj3.localRotation = Quaternion.Euler(-90f, 0, 0);

        rightHand_Obj1.localRotation = Quaternion.Euler(0, 0, 0);
        rightHand_Obj2.localRotation = Quaternion.Euler(-180f, 0, 0);
        rightHand_Obj3.localRotation = Quaternion.Euler(-90f, 0, 0);
    }

    //고정 위치 함수
    public void PosLock()
    {
        if(!_lock)
        {
            _lock = true;
            rov_Pos = playerROV.localPosition;
            //Debug.Log("rov_Pos"+ rov_Pos);
        }
        else
        {
            _lock = false;
        }

        
    }

    //시뮬레이션뷰토글
    public void SimulateionToggle()
    {
        if (simulationToggle.isOn)
        {
            simulationViewOut.SetActive(false);
        }
        else
        {
            simulationViewOut.SetActive(true);
        }
    }

    //리얼뷰토글
    public void RealViewToggle()
    {
        if (realToggle.isOn)
        {
            realViewOut.SetActive(false);
        }
        else
        {
            realViewOut.SetActive(true);
        }
    }



}
