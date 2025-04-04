using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOR_node : CircuitNode
{
    public override void UpdateState()
    {
        if(nodeInputs[0] != null && nodeInputs[1] != null)state = !(nodeInputs[0].getState() | nodeInputs[1].getState());
        else
        {
            if(nodeInputs[0] == null && nodeInputs[1] == null)state = true;
            else if(nodeInputs[0] == null) state = !nodeInputs[1].getState();
            else state = !nodeInputs[0].getState();
        }
        StartCoroutine(Propagate());
    }
}
