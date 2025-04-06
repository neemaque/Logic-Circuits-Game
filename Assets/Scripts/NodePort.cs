using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePort : MonoBehaviour
{
    public int portNumber;
    public bool isInput;
    private Outline outline;
    void Start()
    {
        Outline existingOutline = gameObject.GetComponent<Outline>();
        if (existingOutline != null)
        {
            outline = existingOutline;
        }
        else
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = false;
        }
        
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
    public void Select()
    {
        outline.enabled = true;
    }
    public void deSelect()
    {
        outline.enabled = false;
    }
}
