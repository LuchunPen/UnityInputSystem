using System;

namespace System
{
    public static class SafeInvoker
    {
        public static void SafeInvoke(this Action action)
        {
            if (action != null) action();
        }

        public static void SafeInvoke<T_Input>(this Action <T_Input> a, T_Input arg)
        {
            if (a != null) { a.Invoke(arg); }
        }

        public static T_Output SafeInvoke<T_Output>(this Func<T_Output> f)
        {
            if (f != null) { return f.Invoke(); }
            return default(T_Output);
        }

        public static T_Output SafeInvoke<T_Output, T_Input>(this Func<T_Input, T_Output> f, T_Input arg)
        {
            if (f != null) { return f.Invoke(arg); }
            return default(T_Output);
        }
    }
}
