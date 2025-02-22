using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[] allowedNodes = new bool[32];
    [SerializeField] private UI levelUI;
    private void Start()
    {
        for(int i = 0; i < 32; i++)
        {
            if(allowedNodes[i])
            {
                levelUI.SpawnButton(i);
            }
        }
    }
}
