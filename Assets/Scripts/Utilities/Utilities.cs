using System;

public static class Utilities
{
    /// <summary>
    /// Returns true if two given float values have opposite sign +/-
    /// </summary>
    public static bool OppositeSigns(float x, float y)
    {
        return (x < 0 && y >= 0) || (x >= 0 && y < 0);
    }

    /// <summary>
    /// Returns either +1 or -1
    /// </summary>
    public static int RandomSign()
    {
        Random random = new Random();
        return random.Next(2) == 0 ? -1 : 1;
    }

    /// <summary>
    /// Normalizes float to -1 or 1
    /// </summary>
    public static float NormalizeFloat(float a)
    {
        return a >= 0 ? 1 : -1;
    }
}