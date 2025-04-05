using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string levelName;
    public NodeManager nodeManager;
    public Timer timer;
    public CameraSway cameraSway;
    public Canvas LevelIntroUI;
    public Canvas LevelOutroUI;
    public Text levelTime;
    public Text levelStars;
    public CheckingTextUI checkingTextUI;
    public int[] allowedNodes = new int[32];
    public int[] correctNodeNumber = new int[32];
    public INPUT_node[] inputNodes = new INPUT_node[2];
    public OUTPUT_node[] outputNodes = new OUTPUT_node[2];
    public int numberOfInputs = 2;
    public int numberOfOutputs = 2;
    public AudioSource completeSound;
    public int stars;
    private int[] initialNodeNumber = new int[32];
    
    [System.Serializable]
    public class CorrectSolution
    {
        public bool[] inputs = new bool[2];
        public bool[] outputs = new bool[2];
    }
    
    public List<CorrectSolution> solutionEntries = new List<CorrectSolution>();
    private void Start()
    {
        for(int i=0; i<32; i++)
        {
            initialNodeNumber[i] = allowedNodes[i];
        }
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
            EndLevel();
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
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EndLevel()
    {
        completeSound.Play();
        PlayerPrefs.SetInt(levelName + "_Completed", 1);
        PlayerPrefs.SetString("LastLevel", levelName);
        
        timer.StopTimer();
        float prevTime = PlayerPrefs.GetFloat(levelName + "_Time");
        if(prevTime < 1f)
        {
            prevTime = 999999999999f;
        }
        PlayerPrefs.SetFloat(levelName + "_Time", Mathf.Min(timer.GetElapsedTime(), prevTime));

        int[] placedNodes = PlacedNodes(initialNodeNumber);
        bool perfectSolution = true;
        for(int i=0;i<32;i++)
        {
            if(placedNodes[i] > correctNodeNumber[i])perfectSolution = false;
        }
        if(perfectSolution)stars = 3;
        else stars = 1;
        int prevStars = PlayerPrefs.GetInt(levelName + "_Stars");
        PlayerPrefs.SetInt(levelName + "_Stars", Mathf.Max(stars, prevStars));
        PlayerPrefs.Save();

        Debug.Log(stars);
        LevelOutroUI.gameObject.SetActive(true);
        levelTime.text = timer.FormatTime(timer.GetElapsedTime());
        if(stars == 3)levelStars.text = "★★★";
        else if(stars == 2)levelStars.text = "★★☆";
        else levelStars.text = "★☆☆";
    }
    public int[] PlacedNodes(int[] initial)
    {
        int[] placedNodes = allowedNodes;
        for(int i=0;i<32;i++)
        {
            placedNodes[i] = initial[i] - allowedNodes[i];
        }
        return placedNodes;
    }
}
