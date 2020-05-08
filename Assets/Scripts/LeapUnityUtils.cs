using Leap;
using UnityEngine;

namespace DefaultNamespace
{
    public static class LeapUnityUtils
    {
        public static Vector3 ToUnityVector(this Vector vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
    }
}