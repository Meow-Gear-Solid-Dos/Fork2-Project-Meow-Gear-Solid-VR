using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string callerName = "Puss in Boots";

    // Text uses 3 to 10 lines
    /*[TextArea(3, 10)]
    public string[] sentences;*/

    // Default dialogue dictionary
    // Updates after certain points in game is reached
    public Dictionary<int, List<string>> defDialogue = new Dictionary<int, List<string>>()
    {
        { 0, new List<string> {"Hello Agent C", "This is your mission objective."} },
        { 1, new List<string> {"Damn", "That's a big boy."} }

    };
    
    // Event dialogue dictionary
    public Dictionary<string, List<string>> eventDialogue = new Dictionary<string, List<string>>()
    {
        {"CodecTrigger", new List<string> {"You've found the glock. Good job", "Kill."} },
        {"CodecTrigger1", new List<string> {"Why are you running."} }
    };

}
