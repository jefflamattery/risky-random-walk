using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveListener : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CourseChange _courseChangeEvent;

    void OnMove(InputValue value)
    {
        Vector2 inputHeading = value.Get<Vector2>();
        Vector3 velocityRequest = new Vector3(inputHeading.x, 0f, inputHeading.y);
        Course request;
        
        // if a mouse and keyboard are being used, then the forward direction
        // is always parallel to heading (left input (i.e. 'a' key) means strafe left)

        // if a gamepad is being used, then the forward direction is relative to
        // the stick (left on the stick means left on the screen)

        if(_playerInput.currentControlScheme=="Keyboard"){
            request = new Course(velocityRequest,true);
        } else {
            request = new Course(velocityRequest, false);
        }

        // broadcast this request
        _courseChangeEvent.TriggerEvent(request);
    }

}