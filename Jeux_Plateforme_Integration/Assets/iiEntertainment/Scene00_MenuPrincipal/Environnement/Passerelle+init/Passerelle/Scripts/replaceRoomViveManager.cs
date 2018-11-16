using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Valve.VR;
public class replaceRoomViveManager : MonoBehaviour
{
    /*
     * Attributes
     */
    Vector3 _roomPos;
    float _roomOriUp;
    public GameObject _rightPad;
    public GameObject _rootCalibration;

    // for saving in a new register
    public string _applicationName = "";

    /*
     * Monos
     */
    // Use this for initialization
    public SteamVR_Behaviour_Pose _rightPadPose = null;
    public SteamVR_Action_In _actionTrigger;
    private void Start() {
        /* SteamVR_Input.Dynamic_InitializeActions();
         SteamVR_Input.Dynamic_InitializeInstanceActions();*/
        //  SteamVR_Input.InitializeActions();
        SteamVR_Input.Initialize();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _rootCalibration.SetActive(!_rootCalibration.activeSelf);
            Debug.Log("calibrating");
        }

        if (_rootCalibration.activeSelf)
        {
            if (_actionTrigger.GetActive(_rightPadPose.inputSource))
            {
                SteamVR_Action_Boolean butonVal = (SteamVR_Action_Boolean)_actionTrigger;
                bool pushed = butonVal.GetState(_rightPadPose.inputSource);
                if (pushed)
                {
                    // We are in calibration
                    Debug.Log("clicked");
                    Vector3 posPad = _rightPad.transform.position;
                    Vector3 diffPos = _rootCalibration.transform.position - posPad;
                    //diffPos.y = 0.0f;

                    transform.position += diffPos;

                    float rotPad = _rightPad.transform.eulerAngles.y;
                    float diffRot = _rootCalibration.transform.eulerAngles.y - rotPad;

                    transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + diffRot, 0.0f);
                }
            }
       
        }
    }

    private void OnApplicationQuit()
    {
        _roomPos = transform.localPosition;
        _roomOriUp = transform.localEulerAngles.y;
        saveConfig();
    }

    /*
     * Internals
     */
    void loadConfig()
    {
        float roomPosX  = PlayerPrefs.GetFloat(_applicationName+"roomPosX");
        float roomPosY = PlayerPrefs.GetFloat(_applicationName+"roomPosY");
        float roomPosZ = PlayerPrefs.GetFloat(_applicationName+"roomPosZ");
        _roomPos = new Vector3(roomPosX,roomPosY,roomPosZ);
        _roomOriUp = PlayerPrefs.GetFloat(_applicationName+"roomOriUp");
    }

    void saveConfig()
    {
        PlayerPrefs.SetFloat(_applicationName+"roomPosX", _roomPos.x);
        PlayerPrefs.SetFloat(_applicationName+"roomPosY", _roomPos.y);
        PlayerPrefs.SetFloat(_applicationName+"roomPosZ", _roomPos.z);
        PlayerPrefs.SetFloat(_applicationName+"roomOriUp", _roomOriUp);
    }
}


