using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPUT_node : CircuitNode
{
    public override void UpdateState()
    {
        foreach (var output in nodeOutputs)
        {
            if(output == null)continue;
            output.UpdateState();
        }
    }
    public void Toggle()
    {
        state = !state;
        UpdateState();
    }
}