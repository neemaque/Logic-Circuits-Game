using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private int numberOfButtons;
    [SerializeField] private GameObject nodeButtonPrefab;
    [SerializeField] private Transform nodeButtonsPanel;
    [SerializeField] private NodeManager nodeManager;
    private GameObject[] nodeButtons = new GameObject[32];
    private void Awake()
    {
        numberOfButtons = 0;
    }
    public void SpawnButton(int nodeNumber, int number)
    {
        GameObject newButton = Instantiate(nodeButtonPrefab, nodeButtonsPanel);
        nodeButtons[nodeNumber] = newButton;
        newButton.transform.position += new Vector3(150 * numberOfButtons, 0, 0);
        numberOfButtons++;
        newButton.GetComponent<Button>().onClick.AddListener(() => nodeManager.ChoosePrefab(nodeNumber));
        foreach (Transform child in newButton.transform)
        {
            Text textComponent = child.GetComponent<Text>();
            if (textComponent != null)
            {
                if(child.CompareTag("NodeName"))textComponent.text = nodeName(nodeNumber);
                else textComponent.text = number.ToString();
            }
        }
    }
    public void UpdateNumber(int nodeNumber, int number)
    {
        GameObject button = nodeButtons[nodeNumber];
        foreach (Transform child in button.transform)
        {
            Text textComponent = child.GetComponent<Text>();
            if (textComponent != null)
            {
                if(child.CompareTag("NodeName"))textComponent.text = nodeName(nodeNumber);
                else textComponent.text = number.ToString();
            }
        }
    }
    public string nodeName(int nodeNumber)
    {
        if(nodeNumber == 0)return "INPUT";
        if(nodeNumber == 1)return "OUTPUT";
        if(nodeNumber == 2)return "AND";
        if(nodeNumber == 3)return "NOT";
        if(nodeNumber == 4)return "OR";
        if(nodeNumber == 5)return "FORK";
        else return "NODE";
    }
}
