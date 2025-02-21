using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    public bool state;
    public bool getState()
    {
        return state;
    }
}
