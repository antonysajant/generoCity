using System;
using UnityEngine;
public abstract class States
{
    abstract public void EnterState();
    abstract public void Update();
    abstract public void FixedUpdate();
    abstract public void ExitState();
    
}
