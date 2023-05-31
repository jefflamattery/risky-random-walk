using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Physical Properties")]

public class PhysicalProperties : ScriptableObject
{
    public float turningRate;
    public float speed;
    public float stepSize;
}
