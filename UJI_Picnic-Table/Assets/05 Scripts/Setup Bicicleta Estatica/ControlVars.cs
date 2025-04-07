using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variables para script ExternalControl.cs
[Serializable]
public class ControlVars : MonoBehaviour
{
    public BiciVars biciVars;
    public ManagerVars managerVars;
}

[Serializable]
public class BiciVars
{
    public Vector3 position;
    public Vector3 rotation;
    public float speed;
}

[Serializable]
public class ManagerVars
{
    public string userCode;
}