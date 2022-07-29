using UnityEngine;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers 
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t) {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static bool IsPointInsideCircle(Vector2 point, Vector2 circleCenter, float radius)
    {
        //for debugging purposes we are gonna separate this.
        var distance = Mathf.Abs(Vector2.Distance(point, circleCenter));
        return distance <= radius;
    }
}
