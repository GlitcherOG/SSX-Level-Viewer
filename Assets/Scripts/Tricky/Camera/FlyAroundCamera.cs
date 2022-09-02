using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAroundCamera : MonoBehaviour
{
    public static bool Active = false;
    public float Speed = 100;
    public float CurrentSpeed = 100;
    public float SprintSpeed = 300;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                CurrentSpeed = SprintSpeed;
            }
            else
            {
                CurrentSpeed = Speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * (CurrentSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += transform.forward * (-CurrentSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.right * (-CurrentSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * (CurrentSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position += transform.up * (-CurrentSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                transform.position += transform.up * (CurrentSpeed * Time.deltaTime);
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Active = !Active;
            if(Active)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
