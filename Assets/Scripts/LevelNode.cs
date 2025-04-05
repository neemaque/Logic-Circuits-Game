using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour
{
    [SerializeField] public bool isMenu;
    [SerializeField] public bool isExit;
    [SerializeField] private LevelNode target;
    [SerializeField] private string levelName;
    [SerializeField] public string sceneName;
    [SerializeField] private TextMesh text;
    [SerializeField] private Outline outline;
    [SerializeField] private Canvas ui;
    
    private void Start()
    {
        text.text = levelName;
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
}
