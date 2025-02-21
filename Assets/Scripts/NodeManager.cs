using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> nodePrefabs = new List<GameObject>();
    [SerializeField] private List<CircuitNode> placedNodes = new List<CircuitNode>();
    [SerializeField] private List<Wire> placedWires = new List<Wire>();

    public int chosenPrefab;
    private bool buildingMode;
    private NodePort firstNodePort;
    private void Start()
    {
        buildingMode = false;
        chosenPrefab = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            buildingMode = !buildingMode;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chosenPrefab = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chosenPrefab = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chosenPrefab = 2;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(buildingMode)PlaceNewNode();
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    NodePort selectedPort = hit.collider.GetComponent<NodePort>();
                    if (selectedPort != null)
                    {
                        SelectPort(selectedPort);
                    }
                    INPUT_node selectedNode = hit.collider.GetComponent<INPUT_node>();
                    if (selectedNode != null)
                    {
                        Debug.Log(selectedNode);
                        ToggleInputNode(selectedNode);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CircuitNode selectedNode = hit.collider.GetComponent<CircuitNode>();
                if (selectedNode != null)
                {
                    Debug.Log("deleting" + selectedNode);
                    DeleteNode(selectedNode);
                }
            }
        }
    }
    private void DeleteNode(CircuitNode selectedNode)
    {
        foreach (var nodePort in selectedNode.nodePorts)
        {
            DeleteWire(nodePort);
        }
        placedNodes.Remove(selectedNode);
        Destroy(selectedNode.gameObject);
    }
    private void ToggleInputNode(INPUT_node selectedNode)
    {
        selectedNode.Toggle();
    }
    private void SelectPort(NodePort selectedPort)
    {
        Debug.Log(selectedPort.getParent());
        DeleteWire(selectedPort);
        if(!selectedPort.isInput)
        {
            firstNodePort = selectedPort;
            Debug.Log(firstNodePort.getParent());
        }
        else if(firstNodePort != null)
        {
            CreateWire(firstNodePort, selectedPort);
            ConnectNodes(firstNodePort.getParent(), firstNodePort.portNumber, selectedPort.getParent(), selectedPort.portNumber);
            firstNodePort = null;
        }
    }
    private void PlaceNewNode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject newNodeObj = Instantiate(nodePrefabs[chosenPrefab], hit.point, Quaternion.identity);
            CircuitNode newNode = newNodeObj.GetComponent<CircuitNode>();
            if (newNode != null)
            {
                placedNodes.Add(newNode);
                newNode.UpdateState();
            }
        }
    }
    private void RemoveLastNode()
    {
        if (placedNodes.Count > 0)
        {
            CircuitNode lastNode = placedNodes[placedNodes.Count - 1];
            placedNodes.RemoveAt(placedNodes.Count - 1);
            Destroy(lastNode.gameObject);
        }
    }
    public void ConnectNodes(CircuitNode from, int fromPort, CircuitNode into, int intoPort)
    {
        Debug.Log("connect");
        Debug.Log(from);
        Debug.Log(into);
        into.changeInput(from, intoPort);
        from.changeOutput(into, fromPort);
    }
    private void CreateWire(NodePort start, NodePort end)
    {
        GameObject wireObj = new GameObject("Wire");
        Wire wire = wireObj.AddComponent<Wire>();
        placedWires.Add(wire);
        wire.SetConnection(start, end);
    }
    private void DeleteWire(NodePort nodePort)
    {
        List<Wire> wiresToRemove = new List<Wire>();
        foreach (var wire in placedWires)
        {
            if (wire.startPort == nodePort || wire.endPort == nodePort)
            {
                wire.startPort.getParent().removeOutput(wire.startPort.portNumber);
                wire.endPort.getParent().removeInput(wire.endPort.portNumber);
                wiresToRemove.Add(wire);
            }
        }
        foreach (var wire in wiresToRemove)
        {
            placedWires.Remove(wire);
            Destroy(wire.gameObject);
        }
    }
}