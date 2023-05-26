using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDebug : MonoBehaviour
{
    public void OnEventHeard(string eventName)
    {
        Debug.Log(eventName);
    }


}
