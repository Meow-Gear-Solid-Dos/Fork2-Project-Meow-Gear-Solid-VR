using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string callerName;

    // Text uses 3 to 10 lines
    [TextArea(3, 10)]
    public string[] sentences;
}
