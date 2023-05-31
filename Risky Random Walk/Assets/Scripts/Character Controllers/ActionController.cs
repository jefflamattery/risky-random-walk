using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : ActionEventListener
{
    public Rigidbody root;
    public VectorProperty heading;
    public VectorProperty target;
    public FloatProperty turningRate;
    public BooleanProperty blockInterrupts;
    public PhysicalProperties physicalProperties;

    private Coroutine _master;
    private Coroutine _current;

    public override void OnEventTriggered(Action request)
    {
        if(_master != null){
            StopCoroutine(_master);
        }

        _master = StartCoroutine(Play(request));
    }

    IEnumerator Play(Action request)
    {
        // this action can't play if interrupts are being blocked
        while(blockInterrupts.value){
            yield return new WaitForFixedUpdate();
        }

        // interrupts are no longer being blocked, stop the currently running action
        // and start the newly requested action

        if(_current != null){
            StopCoroutine(_current);
        }

        _current = StartCoroutine(request.ActionRoutine(this));

    }

    
}
