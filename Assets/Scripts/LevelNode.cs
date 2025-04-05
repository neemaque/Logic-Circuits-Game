using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour
{
    [SerializeField] public bool isMenu;
    [SerializeField] public bool isExit;
    [SerializeField] private LevelNode target;
    [SerializeField] private string nodeName;
    [SerializeField] public string levelName;
    [SerializeField] public string sceneName;
    [SerializeField] public int requiredStars;
    [SerializeField] private TextMesh text;
    [SerializeField] private Outline outline;
    [SerializeField] private Canvas ui;
    [SerializeField] private Text starsUi;
    [SerializeField] private Text timeUi;
    
    private void Start()
    {
        if(isMenu)
        {
            text.text = levelName;
        }
        else
        {
            text.text = nodeName;
            if(!isUnlocked())
            {
                starsUi.text = "LOCKED";
                timeUi.text = "Get more stars\nto unlock";
            }
            else if(isCompelete())
            {
                float time = PlayerPrefs.GetFloat(levelName + "_Time");
                timeUi.text = FormatTime(time);
                int stars = PlayerPrefs.GetInt(levelName + "_Stars");
                if(stars == 3)starsUi.text = "★★★";
                else if(stars == 2)starsUi.text = "★★☆";
                else starsUi.text = "★☆☆";
            }
            else
            {
                starsUi.text = "☆☆☆";
                timeUi.text = "Not completed";
            }
        }
        
    }
    public Transform getPosition()
    {
        return transform;
    }
    public void Select()
    {
        outline.enabled = true;
        if(!isMenu)ui.enabled = true;
    }
    public void deSelect()
    {
        outline.enabled = false;
        ui.enabled = false;
    }
    public LevelNode getTarget()
    {
        return target;
    }
    public bool isCompelete()
    {
        if(PlayerPrefs.GetInt(levelName + "_Completed") == 1)return true;
        else return false;
    }
    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public bool isUnlocked()
    {
        int totalStars = 0;
        totalStars = PlayerPrefs.GetInt("lvl1" + "_Stars") + PlayerPrefs.GetInt("lvl2" + "_Stars") + PlayerPrefs.GetInt("lvl3" + "_Stars");
        if(totalStars < requiredStars)return false;
        else return true;
    }
}
