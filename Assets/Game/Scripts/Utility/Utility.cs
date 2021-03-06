﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static IEnumerator DelayAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action();
    }
}
