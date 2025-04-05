using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    private float rotationAmount = 3f;
    private float smoothSpeed = 2f;
    private float screenWidth;
    private float targetRotation = 0f;
    private bool locked;

    void Start()
    {
        transform.rotation = Quaternion.Euler(100, 0, 0);
        screenWidth = Screen.width;
    }

    void Update()
    {
        if(!locked)
        {
            float mouseX = Input.mousePosition.x;

            if (mouseX < screenWidth * 0.25f)
            {
                LookLeft();
            }
            else if (mouseX > screenWidth * 0.75f)
            {
                LookRight();
            }
            else
            {
                targetRotation = 0;
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(53f, targetRotation, transform.rotation.z), Time.deltaTime * smoothSpeed);
    }
    public void Lock()
    {
        locked = true;
    }
    public void Unlock()
    {
        locked = false;
    }
    public void LookLeft()
    {
        targetRotation = -rotationAmount;
    }
    public void LookRight()
    {
        targetRotation = rotationAmount;
    }
    public void LookForward()
    {
        targetRotation = 0;
    }
}
