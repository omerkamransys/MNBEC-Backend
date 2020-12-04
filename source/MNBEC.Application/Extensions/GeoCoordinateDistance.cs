using GeoCoordinatePortable;
using System;

namespace MNBEC.Application.Extensions
{
    /// <summary>
    /// GeoCoordinateDistance inherites GeoCoordinate class to provides implementation for distance calculation.
    /// </summary>
    public class GeoCoordinateDistance : GeoCoordinate
    {
        /// <summary>
        /// GetDistance measure distance between provided Geo Locations.
        /// </summary>
        /// <param name="sourceLatitude"></param>
        /// <param name="sourceLongitude"></param>
        /// <param name="targetLatitude"></param>
        /// <param name="targetLongitude"></param>
        /// <returns></returns>
        public static double GetDistance(decimal sourceLatitude, decimal sourceLongitude, decimal targetLatitude, decimal targetLongitude)
        {
            var sourceCoordinate = new GeoCoordinate(Convert.ToDouble(sourceLatitude), Convert.ToDouble(sourceLongitude));
            var targetCoordinate = new GeoCoordinate(Convert.ToDouble(targetLatitude), Convert.ToDouble(targetLongitude));

            return sourceCoordinate.GetDistanceTo(targetCoordinate);
        }
    }
}