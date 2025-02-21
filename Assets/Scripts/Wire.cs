using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public NodePort startPort;
    public NodePort endPort;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
        if (startPort && endPort)
        {
            lineRenderer.SetPosition(0, startPort.transform.position);
            lineRenderer.SetPosition(1, endPort.transform.position);
        }
    }

    public void SetConnection(NodePort start, NodePort end)
    {
        startPort = start;
        endPort = end;
    }
    public void DestroyWire()
    {
        startPort.getParent().removeOutput(startPort.portNumber);
        endPort.getParent().removeInput(endPort.portNumber);
        Destroy(gameObject);
    }
}
