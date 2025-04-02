using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public NodeManager nodeManager;
    public CameraSway cameraSway;
    public Canvas LevelIntroUI;
    public CheckingTextUI checkingTextUI;
    public int[] allowedNodes = new int[32];
    public INPUT_node[] inputNodes = new INPUT_node[2];
    public OUTPUT_node[] outputNodes = new OUTPUT_node[2];
    public int numberOfInputs = 2;
    public int numberOfOutputs = 2;
    
    [System.Serializable]
    public class CorrectSolution
    {
        public bool[] inputs = new bool[2];
        public bool[] outputs = new bool[2];
    }
    
    public List<CorrectSolution> solutionEntries = new List<CorrectSolution>();
    private void Start()
    {
        OpenIntro();
    }
    public void BeginCheck()
    {
        checkingTextUI.StartCheck();
        nodeManager.BlockInput();
        cameraSway.Lock();
        StartCoroutine(CheckSolution());
    }
    public IEnumerator CheckSolution()
    {
        bool correct = true;
        
        foreach (CorrectSolution correctSolution in solutionEntries)
        {
            bool testPassed = true;
            cameraSway.LookLeft();
            for(int i=0;i < numberOfInputs; i++)
            {
                if(correctSolution.inputs[i] == false)inputNodes[i].SetOff();
                else inputNodes[i].SetOn();
            }
            yield return new WaitForSeconds(1f);
            cameraSway.LookRight();
            yield return new WaitForSeconds(1f);
            for(int i=0;i < numberOfOutputs; i++)
            {
                if(correctSolution.outputs[i] != outputNodes[i].getState())testPassed = false;
            }
            Debug.Log(testPassed);
            if(!testPassed)correct = false;
        }
        checkingTextUI.DoneChecking(correct);
        cameraSway.Unlock();
        if(correct)
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("incorrect");
            nodeManager.unBlockInput();
        }
    }
    public void OpenIntro()
    {
        LevelIntroUI.gameObject.SetActive(true);
        nodeManager.BlockInput();
    }
    public void CloseIntro()
    {
        
        LevelIntroUI.gameObject.SetActive(false);
        nodeManager.unBlockInput();
    }
}
