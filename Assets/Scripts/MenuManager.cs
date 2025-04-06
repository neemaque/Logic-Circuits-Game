using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuCamera menuCamera;
    private LevelNode currentNode;
    [SerializeField] private Transform initialCameraTransform;
    [SerializeField] private AudioSource selectSound;
    private List<LevelNode> placedNodes = new List<LevelNode>();
    private void Start()
    {
        placedNodes = new List<LevelNode>(FindObjectsOfType<LevelNode>());
        string lastLevel = PlayerPrefs.GetString("LastLevel");
        Debug.Log("lastlevel: " +lastLevel);
        if(lastLevel != "")
        {
            foreach(LevelNode node in placedNodes)
            {
                if(node.levelName == lastLevel)
                {
                    Debug.Log("found");
                    SelectLevel(node);
                    break;
                }
            }
        }
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
                    selectSound.Play();
                    if(selectedNode.isMenu)
                    {
                        MenuButton(selectedNode);
                    }
                    else SelectLevel(selectedNode);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            ResetProgress();
        }
    }
    public void SelectLevel(LevelNode selectedNode)
    {
        if(selectedNode == currentNode && selectedNode.isUnlocked())
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
                PlayerPrefs.SetString("LastLevel", "");
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
    private void ResetProgress()
    {
        Debug.Log("resetting");
        PlayerPrefs.SetString("LastLevel", "");
        PlayerPrefs.SetInt("lvl1_Completed", 0);
        PlayerPrefs.SetFloat("lvl1_Time", 0f);
        PlayerPrefs.SetInt("lvl1_Stars", 0);
        
        PlayerPrefs.SetInt("lvl2_Completed", 0);
        PlayerPrefs.SetFloat("lvl2_Time", 0f);
        PlayerPrefs.SetInt("lvl2_Stars", 0);
        
        PlayerPrefs.SetInt("lvl3_Completed", 0);
        PlayerPrefs.SetFloat("lvl3_Time", 0f);
        PlayerPrefs.SetInt("lvl3_Stars", 0);

        
        PlayerPrefs.SetInt("lvl4_Completed", 0);
        PlayerPrefs.SetFloat("lvl4_Time", 0f);
        PlayerPrefs.SetInt("lvl4_Stars", 0);

        PlayerPrefs.SetInt("lvl5a_Completed", 0);
        PlayerPrefs.SetFloat("lvl5a_Time", 0f);
        PlayerPrefs.SetInt("lvl5a_Stars", 0);

        PlayerPrefs.SetInt("lvl6_Completed", 0);
        PlayerPrefs.SetFloat("lvl6_Time", 0f);
        PlayerPrefs.SetInt("lvl6_Stars", 0);

        PlayerPrefs.Save();
    }
}
