using System;
using UnityEngine;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
    public class nrInput : MonoBehaviour
    {

        private ControllerHandEnum m_CurrentDebugHand = ControllerHandEnum.Right;

        public bool _trigger;
        public bool _home;
        public bool _app;

        public Quaternion _rotation;
        public Vector3 _position;
        public Vector3 _gyro;
        public Vector3 _accel;

        public Vector2 _touch;

        public int _battery;

        private void Update()
        {
            if (NRInput.GetAvailableControllersCount() < 2)
            {
                m_CurrentDebugHand = NRInput.DomainHand;
            }
            else
            {
                if (NRInput.GetButtonDown(ControllerHandEnum.Right, ControllerButton.TRIGGER))
                {
                    m_CurrentDebugHand = ControllerHandEnum.Right;
                }
                else if (NRInput.GetButtonDown(ControllerHandEnum.Left, ControllerButton.TRIGGER))
                {
                    m_CurrentDebugHand = ControllerHandEnum.Left;
                }
            }

            if (NRInput.GetButtonDown(m_CurrentDebugHand, ControllerButton.TRIGGER))
            {
              AddExtraInfo("trigger_btn_down");
              _trigger = true;
            }

            if (NRInput.GetButtonDown(m_CurrentDebugHand, ControllerButton.HOME))
            {
              AddExtraInfo("home_btn_down");
              _home = true;
            }

            if (NRInput.GetButtonDown(m_CurrentDebugHand, ControllerButton.APP))
            {
              AddExtraInfo("app_btn_down");
              _app = true;
            }

            if (NRInput.GetButtonUp(m_CurrentDebugHand, ControllerButton.TRIGGER))
            {
              AddExtraInfo("trigger_btn_up");
              _trigger = false;
            }

            if (NRInput.GetButtonUp(m_CurrentDebugHand, ControllerButton.HOME))
            {
              AddExtraInfo("home_btn_up");
              _home = false;
            }

            if (NRInput.GetButtonUp(m_CurrentDebugHand, ControllerButton.APP))
            {
              AddExtraInfo("app_btn_up");
              _app = false;
            }

            _rotation = NRInput.GetRotation(m_CurrentDebugHand);
            _position = NRInput.GetPosition(m_CurrentDebugHand);
            _touch = NRInput.GetTouch(m_CurrentDebugHand);
            _gyro = NRInput.GetGyro(m_CurrentDebugHand);
            _accel = NRInput.GetAccel(m_CurrentDebugHand);
            _battery = NRInput.GetControllerBattery(m_CurrentDebugHand);
        }

        void AddExtraInfo(string _s)
        {

        }

    }
}
