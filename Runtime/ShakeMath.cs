using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /// <param name="inclination">vertical angle in degrees (for inclination = 90 PointOnCircle is calculated)</param>
        /// <param name="azimuth">angle of circle in degrees</param>
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
    }
} 
