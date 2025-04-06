using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Outline[] outlines = new Outline[10];
    public int progress=0;
    private void Update()
    {
        if(progress == 0)
        {
            outlines[0].enabled = true;
            text.text = "Click on INPUT node's OUT 1 port.";
        }
        else if(progress == 1)
        {
            outlines[0].enabled = false;
            outlines[1].enabled = true;
            text.text = "Now click on the OUTPUT node's IN 1 port to connect them.";
        }
        else if(progress == 2)
        {
            outlines[1].enabled = false;
            text.text = "Nice! Now they're connected. Try toggling the INPUT by clicking on the node."; 
        }
        else if(progress == 4)
        {
            text.text = "That doesn't seem to be doing what we want it to. The opposite, actually.";
            progress++;
            StartCoroutine(Wait(5));
        }
        else if(progress == 6)
        {
            outlines[2].enabled = true;
            text.text = "Try selecting a NOT gate from the shelf, place it on the table,\nand connect your INPUT through NOT to OUTPUT to invert the results.";
        }
        else if(progress == 8)
        {
            outlines[2].enabled = false;
            text.text = "Now press on the Check Solution button to see if you got it right!";
        }
    }
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        progress++;
    }
}
