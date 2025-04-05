using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuCamera menuCamera;
    private LevelNode currentNode;
    [SerializeField] private Transform initialCameraTransform;
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                LevelNode selectedNode = hit.collider.GetComponent<LevelNode>();
                if (selectedNode != null)
                {
                    if(selectedNode.isMenu)
                    {
                        MenuButton(selectedNode);
                    }
                    else SelectLevel(selectedNode);
                }
            }
        }
    }
    public void SelectLevel(LevelNode selectedNode)
    {
        if(selectedNode == currentNode)
        {
            Debug.Log("start level");
            SceneManager.LoadScene(selectedNode.sceneName);
        }
        if(currentNode != null)currentNode.deSelect();
        currentNode = selectedNode;
        Transform nodePos = selectedNode.getPosition();
        menuCamera.targetPosition = nodePos;
        selectedNode.Select();
    }
    public void MenuButton(LevelNode selectedNode)
    {
        if(selectedNode == currentNode)
        {
            if(selectedNode.isExit)
            {
                Debug.Log("exiting");
                Application.Quit();
            }
            else SelectLevel(selectedNode.getTarget());
            return;
        }
        Debug.Log("hi");
        if(currentNode != null)currentNode.deSelect();
        currentNode = selectedNode;
        Transform nodePos = selectedNode.getPosition();
        menuCamera.targetPosition = initialCameraTransform;
        selectedNode.Select();
    }
}
