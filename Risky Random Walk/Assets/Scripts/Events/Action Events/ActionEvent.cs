using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Action Event")]
public class ActionEvent : ScriptableObject
{
    private List<ActionEventListener> listeners = new List<ActionEventListener>();
    public void TriggerEvent(Action request)
    {
        for(int i = 0; i < listeners.Count; i++){
            listeners[i].OnEventTriggered(request);
        }
    }

    public void AddListener(ActionEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(ActionEventListener listener)
    {
        listeners.Remove(listener);
    }
}
