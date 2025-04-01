using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INPUT_node : CircuitNode
{
    [SerializeField] private RawImage uiImage;
    [SerializeField] private Texture onTexture;
    [SerializeField] private Texture offTexture;
    public override void UpdateState()
    {
        if(uiImage != null)
        {
            if(state)uiImage.texture = onTexture;
            else uiImage.texture = offTexture;
        }
        StartCoroutine(Propagate());
    }
    public void Toggle()
    {
        state = !state;
        UpdateState();
    }
    public void SetOn()
    {
        state = true;
        UpdateState();
    }
    public void SetOff()
    {
        state = false;
        UpdateState();
    }
}