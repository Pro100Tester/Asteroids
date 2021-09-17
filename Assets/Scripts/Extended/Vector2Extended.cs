using UnityEngine;

namespace Extended
{
    public static class Vector2Extended
    {
        public static Vector2 Clamp(this ref Vector2 value, in Vector2 Min, in Vector2 Max)
        {
            value.x = Mathf.Clamp(value.x, Min.x, Max.x);
            value.y = Mathf.Clamp(value.y, Min.y, Max.y);
            return value;
        }
    }

}

