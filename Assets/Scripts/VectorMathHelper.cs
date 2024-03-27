using UnityEngine;

namespace MalulsArcade
{
    public static class VectorMathHelper
    {
        public static Vector2 AngleToDirVector(float angle)
        {
            return new Vector2(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad)
                );
        }
    }
}