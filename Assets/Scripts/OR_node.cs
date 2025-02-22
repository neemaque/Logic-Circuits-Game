using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OR_node : CircuitNode
{
    public void Awake()
    {

    }
    public override void UpdateState()
    {
        if(nodeInputs[0] != null && nodeInputs[1] != null)state = nodeInputs[0].getState() | nodeInputs[1].getState();
        foreach (var output in nodeOutputs)
        {
            if(output == null)continue;
            output.UpdateState();
        }
    }
}
