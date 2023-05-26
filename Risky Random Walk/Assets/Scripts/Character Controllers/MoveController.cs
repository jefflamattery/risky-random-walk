using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : CourseChangeListener
{
    private static float DIAGONAL = Mathf.PI / 4f;
    public Vector3 _forward = new Vector3(0f, 0f, 1f);
    [SerializeField] private Rigidbody _root;
    [SerializeField] private MovementProperties _playerMovementProperties;

    private Vector3 _heading;
    private Vector3 _velocity = Vector3.zero;
    private bool _velocityFollowsHeading;
    private Coroutine _action;
    private bool _blockInterrupts = false;


    public override void OnEventTriggered(Course request)
    {
        if(request.IgnoreHeading)
        {
            // this is a velocity request

            // if the velocity follows heading, then velocity is not relative to worldspace
            // rotate the velocity by however much the heading is rotated from "forward" in worldspace (k hat)
            if(request.VelocityDependsOnHeading)
            {
                _velocityFollowsHeading = true;
                StartMovementPattern(ChangeVelocity(Quaternion.FromToRotation(_forward, _heading) * request.Velocity));
            } else {
                _velocityFollowsHeading = false;
                StartMovementPattern(ChangeVelocity(request.Velocity));
            }

        } else if(request.IgnoreVelocity)
        {
            // this is a heading request
            _heading = request.Heading;          
            
        } else {
            // this is both a velocity and heading request
        }
    }

    void StartMovementPattern(IEnumerator pattern)
    {
        if(!_blockInterrupts){
            if(_action != null)
            {
                StopCoroutine(_action);
            }

            _action = StartCoroutine(pattern);
        } 
    }

    IEnumerator ChangeVelocity(Vector3 requestedVelocity)
    {
        Vector3 finalVelocity = _playerMovementProperties.maximumSpeed * requestedVelocity;
        float timeRequired = (finalVelocity - _root.velocity).magnitude / _playerMovementProperties.acceleration;
        float timeElapsed = 0f;
        Vector3 acceleration = (finalVelocity - _root.velocity) / timeRequired;

        _velocity = requestedVelocity;

        // accelerate to the new velocity
        while (timeElapsed < timeRequired)
        {
            _root.AddForce(acceleration, ForceMode.Acceleration);
            timeElapsed += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        // clamp the velocity to exactly what it should have been to account for any floating point math errors
        _root.velocity = finalVelocity;


    }
    
}
