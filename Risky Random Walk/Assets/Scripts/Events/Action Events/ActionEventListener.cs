using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEventListener : MonoBehaviour
{
    public ActionEvent actionEvent;

    void OnEnable()
    {
        actionEvent.AddListener(this);
    }

    void OnDisable()
    {
        actionEvent.RemoveListener(this);
    }
    public virtual void OnEventTriggered(Action request){}

}


