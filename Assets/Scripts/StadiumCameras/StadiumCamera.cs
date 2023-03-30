using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StadiumCamera : MonoBehaviour
{
    public abstract IEnumerator IntroPan(Action action);
}
