using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int numberOfButtons;
    [SerializeField] private GameObject nodeButtonPrefab;
    [SerializeField] private Transform nodeButtonsPanel;
    [SerializeField] private NodeManager nodeManager;
    private void Awake()
    {
        numberOfButtons = 0;
    }
    public void SpawnButton(int nodeNumber)
    {
        GameObject newButton = Instantiate(nodeButtonPrefab, nodeButtonsPanel);
        newButton.transform.position += new Vector3(0, 75 * numberOfButtons, 0);
        newButton.GetComponentInChildren<Text>().text = nodeName(nodeNumber);
        newButton.GetComponent<Button>().onClick.AddListener(() => nodeManager.ChoosePrefab(nodeNumber));
        numberOfButtons++;
    }
    public string nodeName(int nodeNumber)
    {
        if(nodeNumber == 0)return "INPUT";
        if(nodeNumber == 1)return "AND";
        if(nodeNumber == 2)return "NOT";
        if(nodeNumber == 3)return "OR";
        else return "NODE";
    }
}
