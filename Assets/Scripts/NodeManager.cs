using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class NodeManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private List<GameObject> nodePrefabs = new List<GameObject>();
    [SerializeField] private List<CircuitNode> placedNodes = new List<CircuitNode>();
    [SerializeField] private List<Wire> placedWires = new List<Wire>();
    [SerializeField] private LayerMask groundLayer;
    
    private bool blockInput;

    public int chosenPrefab;
    private SelectNode currentSelectNode;
    private List<SelectNode> selectNodes = new List<SelectNode>();
    private bool buildingMode;
    private NodePort firstNodePort;

    private int[] allowedNodes = new int[32];
    private void Start()
    {
        allowedNodes = gameManager.allowedNodes;
        for(int i = 0; i < 32; i++)
        {
            if(allowedNodes[i] > 0)
            {
                nodeUI.SpawnButton(i, allowedNodes[i]);
            }
        }
        buildingMode = false;
        chosenPrefab = 0;
        FindAllNodes();
    }
    public void BlockInput()
    {
        blockInput = true;
    }
    public void unBlockInput()
    {
        blockInput = false;
    }
    private void FindAllNodes()
    {
        placedNodes = new List<CircuitNode>(FindObjectsOfType<CircuitNode>());
        selectNodes = new List<SelectNode>(FindObjectsOfType<SelectNode>());
        
    }
    void Update()
    {
        foreach(SelectNode selectNode in selectNodes)
        {
            selectNode.setNumber(allowedNodes[selectNode.id]);
        }
        if(blockInput)
        {
            buildingMode = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && buildingMode)
        {
            buildingMode = false;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            //if(IsPointerOverUI())return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                NodePort selectedPort = hit.collider.GetComponent<NodePort>();
                if (selectedPort != null && !buildingMode)
                {
                    Debug.Log("Selected port:" + selectedPort);
                    SelectPort(selectedPort);
                }
                INPUT_node selectedNode = hit.collider.GetComponent<INPUT_node>();
                if (selectedNode != null && !buildingMode)
                {
                    Debug.Log("Selected node:" + selectedNode);
                    ToggleInputNode(selectedNode);
                }
                SelectNode selectNode = hit.collider.GetComponent<SelectNode>();
                if (selectNode != null)
                {
                    ChoosePrefab(selectNode.id);
                    if(currentSelectNode != null)currentSelectNode.deSelect();
                    selectNode.Select();
                    currentSelectNode = selectNode;
                }
            }
            if(buildingMode)PlaceNewNode();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(IsPointerOverUI())return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CircuitNode selectedNode = hit.collider.GetComponent<CircuitNode>();
                if (selectedNode != null && !selectedNode.nonDeletable)
                {
                    Debug.Log("deleting" + selectedNode);
                    DeleteNode(selectedNode);
                }
            }
        }
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    } 
    private void DeleteNode(CircuitNode selectedNode)
    {
        selectedNode.SetState(false);
        List<CircuitNode> nodeOutputs = selectedNode.nodeOutputs;
        foreach(var nodeOutput in nodeOutputs)
        {
            if(nodeOutput != null)nodeOutput.UpdateState();
        }
        foreach (var nodePort in selectedNode.nodePorts)
        {
            DeleteWire(nodePort);
        }
        placedNodes.Remove(selectedNode);
        int nodeID = selectedNode.type;
        allowedNodes[nodeID]++;
        nodeUI.UpdateNumber(nodeID, allowedNodes[nodeID]);
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
            if(firstNodePort != null) firstNodePort.deSelect();
            selectedPort.Select();
            firstNodePort = selectedPort;
            Debug.Log(firstNodePort.getParent());
        }
        else if(firstNodePort != null)
        {
            CreateWire(firstNodePort, selectedPort);
            ConnectNodes(firstNodePort.getParent(), firstNodePort.portNumber, selectedPort.getParent(), selectedPort.portNumber);
            firstNodePort.deSelect();
            firstNodePort = null;
        }
    }
    private void PlaceNewNode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer) && !IsSpaceOccupied(hit.point))
        {
            GameObject newNodeObj = Instantiate(nodePrefabs[chosenPrefab], hit.point, Quaternion.identity);
            CircuitNode newNode = newNodeObj.GetComponent<CircuitNode>();
            if (newNode != null)
            {
                placedNodes.Add(newNode);
                newNode.UpdateState();
                buildingMode = false;
                allowedNodes[chosenPrefab]--;
                if(currentSelectNode != null) currentSelectNode.deSelect();
                nodeUI.UpdateNumber(chosenPrefab, allowedNodes[chosenPrefab]);
            }
        }
    }
    bool IsSpaceOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<CircuitNode>() != null)
            {
                return true;
            }
        }
        return false;
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

    public void ChoosePrefab(int prefabNumber)
    {
        if(allowedNodes[prefabNumber] == 0)return;
        Debug.Log("chosen prefab " + prefabNumber);
        buildingMode = true;
        chosenPrefab = prefabNumber;
    }
    
    
}