using System.Collections.Generic;
using UnityEngine;

public class CircuitNode : Node
{
    public List<CircuitNode> nodeInputs = new List<CircuitNode>();
    public List<CircuitNode> nodeOutputs = new List<CircuitNode>();
    [SerializeField] public List<NodePort> nodePorts = new List<NodePort>();
    public bool nonDeletable;
    public string type;
    public void Update()
    {
        setColor(state);
    }
    public void SetState(bool newState)
    {
        state = newState;
        UpdateState();
    }
    public virtual void UpdateState()
    {
        if(nodeOutputs[0] == null)return;
        foreach (var outputs in nodeOutputs)
        {
            outputs.UpdateState();
        }
    }
    public void setColor(bool state)
    {
        Color newColor = Color.white;
        if(state)newColor = Color.red;
        Transform child = transform.Find("Node Mesh");
        if (child != null)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }
        }
    }

    public void changeInput(CircuitNode newInput, int portNumber)
    {
        nodeInputs[portNumber] = newInput;
        UpdateState();
    }
    public void changeOutput(CircuitNode newOutput, int portNumber)
    {
        nodeOutputs[portNumber] = newOutput;
    }

    public void removeInput(int portNumber)
    {
        nodeInputs[portNumber] = null;
        UpdateState();
    }
    public void removeOutput(int portNumber)
    {
        nodeOutputs[portNumber] = null;
    }
}
