using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class localPosition : MonoBehaviour
{

    public struct userAttributes {}
    public struct appAttributes {}

    public float _z = 1;
    public float _y = 0;

    public Transform _cube;

    public bool _lookat;

    void Awake()
    {
      ConfigManager.FetchCompleted += setPosition;
      ConfigManager.FetchConfigs<userAttributes,appAttributes>(new userAttributes(),new appAttributes());
    }

    void setPosition(ConfigResponse response)
    {
        _z = ConfigManager.appConfig.GetFloat("cubePosition");
        _y = ConfigManager.appConfig.GetFloat("cubeRotation");
        _lookat = ConfigManager.appConfig.GetBool("lookat");

        _cube.localPosition = new Vector3(0,0,_z);
        _cube.localEulerAngles = new Vector3(0,_y,0);
    }

    void Update()
    {
      if(Input.GetKeyUp(KeyCode.G))
      {
        ConfigManager.FetchConfigs<userAttributes,appAttributes>(new userAttributes(),new appAttributes());
      }
    }

    void OnDestroy()
    {
      ConfigManager.FetchCompleted -= setPosition;
    }
}
