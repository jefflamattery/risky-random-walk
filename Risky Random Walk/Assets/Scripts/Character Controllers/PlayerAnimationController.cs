using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : ActionEventListener
{
    public Rigidbody root;
    public PhysicalProperties physicalProperties;
    public Animator animator;
    public override void OnEventTriggered(Action request)
    {
        // play the animation routine
        StartCoroutine(request.AnimationRoutine(this));
    }
    
}
