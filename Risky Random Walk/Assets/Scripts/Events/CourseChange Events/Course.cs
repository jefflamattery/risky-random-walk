using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course{
    private Vector3 _velocity;
    private Vector3 _heading;
    private bool _velocityDependsOnHeading;
    private bool _ignoreVelocity;
    private bool _ignoreHeading;

    public Vector3 Velocity{
        get=>_velocity;
    }

    public Vector3 Heading{
        get=>_heading;
    }

    public bool VelocityDependsOnHeading{
        get=>_velocityDependsOnHeading;
    }

    public bool IgnoreVelocity{
        get=>_ignoreVelocity;
    }
    public bool IgnoreHeading{
        get=>_ignoreHeading;
    }

    public Course(Vector3 velocity, Vector3 heading, bool velocityDependsOnHeading)
    {
        _velocity = velocity;
        _heading = heading;
        _velocityDependsOnHeading = velocityDependsOnHeading;
        _ignoreHeading = false;
        _ignoreVelocity = false;
    }

    public Course(Vector3 velocity, bool velocityDependsOnHeading)
    {
        _velocity = velocity;
        _heading = Vector3.zero;
        _velocityDependsOnHeading = velocityDependsOnHeading;
        _ignoreHeading = true;
        _ignoreVelocity = false;
    }

    public Course(Vector3 heading)
    {
        _velocity = Vector3.zero;
        _heading = heading;
        _velocityDependsOnHeading = false;
        _ignoreHeading = false;
        _ignoreVelocity = true;
    }
    
}

