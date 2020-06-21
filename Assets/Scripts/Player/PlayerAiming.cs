using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public float turnSpeed = 1f;

    Camera mMainCamera;

    private void Start()
    {
        mMainCamera = Camera.main;

        //Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
     /*   if (mLastDirection.y > 0.1f)
        {
            float yawCamera = mMainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        }*/
    }
}

