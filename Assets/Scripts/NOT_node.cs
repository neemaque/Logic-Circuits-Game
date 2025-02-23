using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOT_node : CircuitNode
{
    public override void UpdateState()
    {
        if(nodeInputs[0] != null)state = !(nodeInputs[0].getState());
        else
        {
            state = true;
        }
        foreach (var output in nodeOutputs)
        {
            if(output == null)continue;
            output.UpdateState();
        }
    }
}
