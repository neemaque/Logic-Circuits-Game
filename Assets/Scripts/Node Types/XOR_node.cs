using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR_node : CircuitNode
{
    public override void UpdateState()
    {
        if(nodeInputs[0] != null && nodeInputs[1] != null)state = nodeInputs[0].getState() ^ nodeInputs[1].getState();
        else 
        {
            if(nodeInputs[0] == null && nodeInputs[1] == null)state = false;
            else if(nodeInputs[0] == null)state = nodeInputs[1].getState();
            else if(nodeInputs[1] == null)state = nodeInputs[0].getState();
            else state = true;
        }
        StartCoroutine(Propagate());
    }
}
