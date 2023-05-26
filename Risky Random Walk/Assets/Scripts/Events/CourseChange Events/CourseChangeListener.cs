using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CourseChangeListener : MonoBehaviour
{
    public CourseChange courseChangeEvent;

    void OnEnable()
    {
        courseChangeEvent.AddListener(this);
    }

    void OnDisable()
    {
        courseChangeEvent.RemoveListener(this);
    }
    public virtual void OnEventTriggered(Course request){}

}


