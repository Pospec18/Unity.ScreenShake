using UnityEngine;

namespace Pospec.ScreenShake
{
    /// <summary>
    /// Helper class for math used in shakes
    /// </summary>
    public static class ShakeMath
    {
        /// <summary>
        /// Converts Polar coordinates to Cartesian (x, y) coordinates
        /// </summary>
        /// <param name="angle">angle between (1, 0) and position of point in degreed</param>
        /// <param name="radius">radius of the circle</param>
        /// <returns>Point on circle</returns>
        public static Vector2 PointOnCircle(float angle, float radius)
        {
            angle *= Mathf.PI / 180;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        /// <summary>
        /// Converts Spherical coordinates to Cartesian (x, y, z) coordinates
        /// </summary>
        /// <param name="inclination">angle between forward direction and line from center of the sphere to the point</param>
        /// <param name="azimuth">angle on circle where point lies, circle is orthogonal to forward direction</param>
        /// <param name="radius">radius of the sphere</param>
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

        /// <summary>
        /// Calculates rotation to point in sphere from Vector3.forward
        /// </summary>
        /// <param name="inclination">angle between forward direction and line from center of the sphere to the point</param>
        /// <param name="azimuth">angle on circle where point lies, circle is ortogonal to forward direction</param>
        /// <param name="up">by what angle rotate up direction</param>
        /// <returns></returns>
        public static Quaternion RotationOnSphere(float inclination, float azimuth, float up)
        {
            Vector3 point = PointOnSphere(inclination, azimuth, 1);
            return Quaternion.FromToRotation(Vector3.forward, point) * Quaternion.AngleAxis(up, Vector3.forward);
        }

        /// <summary>
        /// Clamps angle to range from -180 to 180
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>Angle between -180 and 180 degrees</returns>
        public static float ClampToRotationAngle(this float angle)
        {
            angle = angle < 0 ? angle + 360 : angle;
            angle = angle < 180 ? angle : angle - 360;
            return angle;
        }

        /// <summary>
        /// Clamps all vector components to range from -180 to 180
        /// </summary>
        /// <param name="angles">vector of angles</param>
        /// <returns>Vector of angles between -180 and 180 degrees</returns>
        public static Vector3 ClampToEulerRotation(this Vector3 angles)
        {
            angles.x = ClampToRotationAngle(angles.x);
            angles.y = ClampToRotationAngle(angles.y);
            angles.z = ClampToRotationAngle(angles.z);
            return angles;
        }

        /// <summary>
        /// Scales vector of angles by scale factor
        /// </summary>
        /// <param name="angles">vector of angles</param>
        /// <param name="scale">scale factor</param>
        /// <returns></returns>
        public static Vector3 ScaleAngles(this Vector3 angles, float scale)
        {
            return ClampToEulerRotation(angles) * scale;
        }

        /// <summary>
        /// Convert position offset to euler rotation
        /// </summary>
        /// <param name="offset">position offset</param>
        /// <returns>euler rotation</returns>
        public static Vector3 ToRotation(this Vector3 offset)
        {
            return new Vector3(-offset.y, offset.x, offset.z);
        }

        /// <summary>
        /// Converts euler rotation to position position offset
        /// </summary>
        /// <param name="rotation">euler rotation</param>
        /// <returns>position offset</returns>
        public static Vector3 ToOffset(this Vector3 rotation)
        {
            return new Vector3(-rotation.y, rotation.x, rotation.z);
        }
    }
} 
