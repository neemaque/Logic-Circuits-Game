using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckingTextUI : MonoBehaviour
{
    [SerializeField] private Text text;
    private bool checking;

    public void StartCheck()
    {
        text.gameObject.SetActive(true);
        text.text = "Checking.";
        checking = true;
        StartCoroutine(LoopingCoroutine());
    }
    public void DoneChecking(bool result)
    {
        checking = false;
        StopCoroutine(LoopingCoroutine());
        if(result == false)
        {
            text.text = "Wrong! Try again";
            StartCoroutine(Deactivate());
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        text.gameObject.SetActive(false);
    }
    IEnumerator LoopingCoroutine()
    {
        while (checking)
        {
            yield return new WaitForSeconds(0.5f);
            if(text.text == "Checking...")
            {
                text.text = "Checking.";
            }
            else if(text.text == "Checking.")
            {
                text.text = "Checking..";
            }
            else if(text.text == "Checking..")
            {
                text.text = "Checking...";
            }
        }
    }
}