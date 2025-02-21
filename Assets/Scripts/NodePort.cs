using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePort : MonoBehaviour
{
    public int portNumber;
    public bool isInput;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public bool getState()
    {
        return false;
    }
    public CircuitNode getParent()
    {
        return transform.parent?.gameObject.GetComponent<CircuitNode>();
    }
}
