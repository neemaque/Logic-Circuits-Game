using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OUTPUT_node : CircuitNode
{
    public override void UpdateState()
    {
        if(nodeInputs[0] != null)state = (nodeInputs[0].getState());
    }
}
