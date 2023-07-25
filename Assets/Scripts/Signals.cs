using System;

public static class Signals
{
    /// <summary>
    /// Gameplay delta time could be use by gameplay entities instead of time.Deltatime
    /// </summary>
    public static Func<float> GameplayDeltaTime;
    /// <summary>
    /// Signal raised by gameplay entities
    /// </summary>
    public static EventHandler TryToPause;
    /// <summary>
    /// Signal raised by gameplay entities
    /// </summary>
    public static EventHandler TryToUnpause;
    /// <summary>
    /// Signal raised by gameplay manager and initialized game Pause
    /// </summary>
    public static EventHandler<bool> OnPause;
    /// <summary>
    /// Gameplay pause state
    /// </summary>
    public static Func<bool> IsPaused;
    /// <summary>
    /// retunrs opened Menu windows, if no module connected to this func, returns 0
    /// </summary>
    public static Func<object, int> CloseMenuWindow;
    /// <summary>
    /// retunrs opened Menu windows, if no module connected to this func, returns 0
    /// </summary>
    public static Func<object, int> OpenMenuWindow;
}
