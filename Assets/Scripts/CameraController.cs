using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private float rotationInput = 0f;
    [SerializeField] private float maxInput = 1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(rotationInput < maxInput)rotationInput += 1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if(rotationInput > -maxInput)rotationInput -= 1 * Time.deltaTime;
        }
        else
        {
            if(rotationInput > 0.1)rotationInput -= 5 * Time.deltaTime;
            else if(rotationInput < -0.1)rotationInput += 5 * Time.deltaTime;
            else rotationInput = 0;
        }

        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);
    }
}
