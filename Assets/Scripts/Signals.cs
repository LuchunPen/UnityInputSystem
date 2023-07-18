using System;
using System.Collections.Generic;
using UnityEngine;

public static class Signals
{
    public static EventHandler<bool> OnPause;
    public static Func<bool> IsPaused;

    public static Func<float> GameplayDeltaTime;
}
