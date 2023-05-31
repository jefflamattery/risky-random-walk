using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardListener : MonoBehaviour
{

    [SerializeField] private VectorProperty _heading;
    [SerializeField] private FloatProperty _turningRate;
    [SerializeField] private Rigidbody _root;
    [SerializeField] private ActionEvent _player;
    [SerializeField] private float _cursorErrorRadius = 20f;
    [SerializeField] private float _mouseTurnRate;
    private bool _isMoving = false;

    void OnStartMove()
    {
        _isMoving = true;
        StartCoroutine(UpdateHeading());

        // request a new move action
        _player.TriggerEvent(new MoveAction());

    }

    void OnStopMove()
    {
        _isMoving = false;
        _heading.value = Vector3.zero;
    }


    IEnumerator UpdateHeading()
    {
        Vector2 mousePosition;
        Vector3 rootScreenPosition = Vector3.zero;
        Vector3 heading;
        float headingMagnitude;

        while(_isMoving){

            mousePosition = Mouse.current.position.ReadValue();
            rootScreenPosition = Camera.main.WorldToScreenPoint(_root.transform.position);
            heading = new Vector3(mousePosition.x - rootScreenPosition.x, 0f, mousePosition.y - rootScreenPosition.y);
            
            // if the heading magnitude is less than cursor error, then this is effectively a stop command
            headingMagnitude = heading.magnitude;
            if(headingMagnitude <= _cursorErrorRadius){
                heading = Vector3.zero;
            } else {
                // use the heading magnitude to set the turning rate
                _turningRate.value = _mouseTurnRate / Mathf.Sqrt(headingMagnitude);

                // normalize the heading
                heading.Normalize();
            }

            
            _heading.value = heading;

            yield return new WaitForFixedUpdate();
        }
    }

    
}
