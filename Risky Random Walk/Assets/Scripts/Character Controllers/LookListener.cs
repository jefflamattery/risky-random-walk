using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookListener : MonoBehaviour
{
    [SerializeField] Rigidbody _root;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] private CourseChange _courseChangeEvent;

    private Vector3 _heading;
    private Vector3 _initialHeading = new Vector3(0f, 0f, 1f);


    void OnLook(InputValue value)
    {
        Vector2 aimPosition = value.Get<Vector2>();
        Vector3 heading;
        
        
        if(_playerInput.currentControlScheme=="Keyboard"){
            heading = GetMouseHeading(aimPosition);
        } else {
            heading = new Vector3(aimPosition.x, 0f, aimPosition.y);
        }

        heading.Normalize();

        if(heading.sqrMagnitude == 0f)
        {
            heading = _heading;
        } else {
            _heading = heading;
        }

        // Broadcast a new course change event
        _courseChangeEvent.TriggerEvent(new Course(heading));
        
    }

    private Vector3 GetMouseHeading(Vector2 inputHeading)
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(_root.transform.position);
        Vector2 playerScreenPosition = new Vector2(playerPosition.x, playerPosition.y);
        Vector3 heading = new Vector3(inputHeading.x - playerScreenPosition.x, 0f, inputHeading.y - playerScreenPosition.y);

        return heading;
    }


}
