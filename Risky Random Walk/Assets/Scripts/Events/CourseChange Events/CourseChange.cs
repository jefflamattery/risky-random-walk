using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/CourseChange Event")]
public class CourseChange : ScriptableObject
{
    private List<CourseChangeListener> listeners = new List<CourseChangeListener>();
    public void TriggerEvent(Course request)
    {
        for(int i = 0; i < listeners.Count; i++){
            listeners[i].OnEventTriggered(request);
        }
    }

    public void AddListener(CourseChangeListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(CourseChangeListener listener)
    {
        listeners.Remove(listener);
    }
}
