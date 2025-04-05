using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNode : MonoBehaviour
{
    public int id;
    [SerializeField] private string name = "";
    [SerializeField] private TextMesh text;
    [SerializeField] private Outline outline;
    [SerializeField] private Text numberUI;

    private void Start()
    {
        text.text = name;
    }
    public void Select()
    {
        outline.enabled = true;
    }
    public void deSelect()
    {
        outline.enabled = false;
    }
    public void setNumber(int number)
    {
        numberUI.text = number.ToString();
    }
}
