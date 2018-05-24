using System;

public static class MulticastDelegateExtensions
{
    public static int GetLength(this MulticastDelegate self)
    {
        if (self == null || self.GetInvocationList() == null)
        {
            return 0;
        }
        return self.GetInvocationList().Length;
    }
}