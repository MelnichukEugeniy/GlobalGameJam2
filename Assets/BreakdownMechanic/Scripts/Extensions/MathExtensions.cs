using UnityEngine;

public static class MathExtensions
{
    public static float Map(this float value, float oldA, float oldB, float newA, float newB)
    {
        return Mathf.Lerp(newA, newB, Mathf.InverseLerp(oldA, oldB, value));
    }
}