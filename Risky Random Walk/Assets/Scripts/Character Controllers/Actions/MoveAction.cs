using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    private bool _isMoving;
    public override IEnumerator AnimationRoutine(PlayerAnimationController master)
    {
        float speedScale = 1f;

        while(true){

            speedScale = master.physicalProperties.speed;

            master.animator.SetBool("isRunning", _isMoving);
            master.animator.SetFloat("runSpeed", speedScale);

            yield return new WaitForFixedUpdate();
        }
    }

    public override IEnumerator ActionRoutine(ActionController master)
    {
        // constantly rotate the root toward the heading
        // always move in the forward direction, transformed by the root's rotation
        // a heading with a magnitude of 0 means stop

        Vector3 step, stepPlus, stepMinus;
        Vector3 heading, velocity;
        float headingMagnitude;
        float dTheta;
        float speed = master.physicalProperties.speed;
        float stepSize = master.physicalProperties.stepSize;
        float middleAlignment;
        float leftAlignment;
        float rightAlignment;

        _isMoving = true;
        

        while(true)
        {
            heading = master.heading.value;
            headingMagnitude = heading.magnitude;

            if(headingMagnitude > 0f){

                _isMoving = true;

                // the turning rate can vary with time, so it needs to be captured every cycle
                dTheta = master.turningRate.value;

                // use three equal-magnitude vectors to determine which direction is the correct one to move in
                // by looking for the vector that is closest to the heading
                step = master.root.rotation * (stepSize * Vector3.forward);
                stepPlus = Quaternion.Euler(0f, dTheta, 0f) * step;
                stepMinus = Quaternion.Euler(0f, -dTheta, 0f) * step;

                middleAlignment = Vector3.Dot(step, heading);
                leftAlignment = Vector3.Dot(stepPlus, heading);
                rightAlignment = Vector3.Dot(stepMinus, heading);

                // if the middle alignment is the largest, then the heading points in the direction that the root should be moving
                if(middleAlignment >= leftAlignment && middleAlignment >= rightAlignment){
                    velocity = speed * heading;
                } else {
                    if(leftAlignment > rightAlignment){
                        velocity = speed / stepSize * stepPlus;
                    } else {
                        velocity = speed / stepSize * stepMinus;
                    }
                }

                

                // rotate the root so that is always facing in the direction of the velocity
                master.root.transform.rotation = Quaternion.FromToRotation(Vector3.forward, velocity);

            } else {
                velocity = Vector3.zero;
                _isMoving = false;
            }

            
            master.root.velocity = velocity;

            yield return new WaitForFixedUpdate();
        }

    }
}
