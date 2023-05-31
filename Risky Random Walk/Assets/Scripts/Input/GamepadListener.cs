using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadListener : MonoBehaviour
{
    [SerializeField] private VectorProperty _heading;
    [SerializeField] private FloatProperty _turningRate;
    [SerializeField] private Rigidbody _root;
    [SerializeField] private ActionEvent _player;
    [SerializeField] private float _gamepadTurningRate;

    private Coroutine _update;
    private bool _isMoving;

    void OnStickMove(InputValue value)
    {
        Vector2 stickHeading = value.Get<Vector2>();
        Vector3 heading = new Vector3(stickHeading.x, 0f, stickHeading.y);
        float headingMagnitude = heading.magnitude;

        // update the heading with the input vector

        if(headingMagnitude <= 0f){
            _isMoving = false;
            _heading.value = Vector3.zero;
        } else {
            if(!_isMoving){
                _isMoving = true;
                
                // the root wasn't moving, and now there is a request to move
                // that will require a new move action to be triggered
                _player.TriggerEvent(new MoveAction());
            }

            // use the heading magnitude to set the turning rate
            _turningRate.value = _gamepadTurningRate * headingMagnitude;
            heading.Normalize();
            _heading.value = heading;
        }
        
    }


}
