using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OUTPUT_node : CircuitNode
{
    [SerializeField] private RawImage uiImage;
    [SerializeField] private Texture onTexture;
    [SerializeField] private Texture offTexture;
    public override void UpdateState()
    {
        if(nodeInputs[0] != null)state = (nodeInputs[0].getState());
        else state = false;
        
        if(uiImage != null)
        {
            if(state)uiImage.texture = onTexture;
            else uiImage.texture = offTexture;
        }
    }
}
