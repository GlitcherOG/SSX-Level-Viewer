using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Player
{
    public class MouseLook : MonoBehaviour
    {
        [Range(0, 200)] //Range for the float
        public float sensitivity = 15; //How sensitive the look speed is
        public Vector3 rotation = new Vector3();
        void Start()
        {

        }
        void Update()
        {
            if (FlyAroundCamera.Active)
            {
                rotation.y += Input.GetAxis("Mouse X") * sensitivity;
                rotation.x += -Input.GetAxis("Mouse Y") * sensitivity;
                transform.eulerAngles = rotation;
                SkyboxManager.Instance.Skybox.transform.eulerAngles = new Vector3(-90, 0, 0);
                //transform.Rotate(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
            }
        }
    }
}
