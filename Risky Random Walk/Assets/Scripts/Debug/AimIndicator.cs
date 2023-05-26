using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : CourseChangeListener
{
    [SerializeField] private GameObject _aimIndicator;
    [SerializeField] private Rigidbody _root;
    [SerializeField] private float _radius;

    private Vector3 _heading = Vector3.zero;
    public override void OnEventTriggered(Course request)
    {
        Vector3 heading;

        if(request.IgnoreHeading){
            heading = _heading;
        } else {
            heading = request.Heading;
            _heading = heading;
        }

        // move the aim indicator to the position indicated by the heading
        _aimIndicator.transform.position = _root.transform.position + _radius * heading;
    }
    
}
