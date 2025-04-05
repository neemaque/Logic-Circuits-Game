using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform targetPosition;
    public float speed = 5f;
    private void Start()
    {
        targetPosition = transform;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
