using UnityEngine;
using UnityEngine.UIElements;

namespace Pospec.ScreenShake
{
    public static class ShakeMath
    {
        public static Vector2 PointOnCircle(float angle, float radius)
        {
            angle *= Mathf.PI / 180;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        /// <summary>
        /// Converts shperical coordinates to Cartesian (x, y, z) coordinates
        /// </summary>
        /// <param name="inclination">vertical angles.x in degrees (for inclination = 90 PointOnCircle is calculated)</param>
        /// <param name="azimuth">angles.x of circle in degrees</param>
        /// <param name="radius">radius of the circle</param>
        /// <returns>Point on sphere</returns>
        public static Vector3 PointOnSphere(float inclination, float azimuth, float radius)
        {
            inclination *= Mathf.PI / 180;
            azimuth *= Mathf.PI / 180;

            return new Vector3(
                radius * Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                radius * Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                radius * Mathf.Cos(inclination));
        }

        public static Quaternion RotationOnSphere(float inclination, float azimuth, float up)
        {
            Vector3 point = PointOnSphere(inclination, azimuth, 1);
            return Quaternion.FromToRotation(Vector3.forward, point) * Quaternion.AngleAxis(up, Vector3.forward);
        }

        public static float NormalizeRotationAngle(float angle)
        {
            angle = angle < 0 ? angle + 360 : angle;
            angle = angle < 180 ? angle : angle - 360;
            return angle;
        }

        public static Vector3 NormalizeRotationAngles(Vector3 angles)
        {
            angles.x = NormalizeRotationAngle(angles.x);
            angles.y = NormalizeRotationAngle(angles.y);
            angles.z = NormalizeRotationAngle(angles.z);
            return angles;
        }

        public static Vector3 ScaleAngles(Vector3 angles, float scale)
        {
            return NormalizeRotationAngles(angles) * scale;
        }
    }
} 
