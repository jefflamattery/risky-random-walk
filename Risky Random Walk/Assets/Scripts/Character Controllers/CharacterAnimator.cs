using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : CourseChangeListener
{
    static float DIAGONAL = Mathf.Sqrt(2f) / 2f;
    [SerializeField] private Rigidbody _root;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runningParameter;
    [SerializeField] private string _directionParameter;
    [SerializeField] private string _slideParameter;
    [SerializeField] private Vector3 _forward = new Vector3(0f, 0f, 1f);
    
    private Vector3 _heading;
    private Quaternion _initialRotation;

    void Start(){
        _initialRotation = _root.transform.rotation;
    }

    public override void OnEventTriggered(Course request)
    {
        float velocityMagnitude;
        float alignment;
        float orthogonality;

        // for now, rotate the model so that it faces the heading in the last heading request
        if(!request.IgnoreHeading){
            _heading = request.Heading;
            _root.transform.rotation = Quaternion.FromToRotation(_forward, _heading) * _initialRotation;

            // decide whether the character is forward running, backward running, or strafing
            velocityMagnitude = _root.velocity.magnitude;

            if(velocityMagnitude > 0f){
                alignment = Vector3.Dot(_heading, _root.velocity) / velocityMagnitude;
                if(alignment > DIAGONAL){
                    // forward run
                    _animator.SetInteger(_directionParameter, 0);
                } else if(alignment < -DIAGONAL){
                    // backward run
                    _animator.SetInteger(_directionParameter, 2);
                } else {
                    // strafe
                    orthogonality = Vector3.Dot(Vector3.Cross(_heading, _root.velocity), Vector3.up);
                    if(orthogonality < 0f){
                        // left strafe
                        Debug.Log("left");
                        _animator.SetInteger(_directionParameter, 1);
                    } else {
                        // right strafe
                        Debug.Log("right");
                        _animator.SetInteger(_directionParameter, 3);
                    }
                }
            }
        }

        if(!request.IgnoreVelocity){
            // for now, switch between running and idle
            if(request.Velocity.magnitude == 0f){
                // stop moving
                _animator.SetTrigger(_slideParameter);
                _animator.SetBool(_runningParameter, false);
            } else {
                _animator.ResetTrigger(_slideParameter);
                // ensure that the character is moving
                _animator.SetInteger(_directionParameter, 0);
                _animator.SetBool(_runningParameter, true);
            }
        }
    }
}
