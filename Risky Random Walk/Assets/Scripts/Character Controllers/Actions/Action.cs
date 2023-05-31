using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public virtual IEnumerator ActionRoutine(ActionController master){
        yield return null;
    }

    public virtual IEnumerator AnimationRoutine(PlayerAnimationController master){
        yield return null;
    }
}
