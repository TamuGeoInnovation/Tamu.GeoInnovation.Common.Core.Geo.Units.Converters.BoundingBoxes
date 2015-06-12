using System;
using USC.GISResearchLab.Common.GeographicObjects.Coordinates;
using USC.GISResearchLab.Common.Geographics.DistanceFunctions;
using USC.GISResearchLab.Common.Geometries.BoundingBoxes;

namespace USC.GISResearchLab.Common.Geographics.Units
{
    public class BoundingBoxUnitConverter
    {

        public static double ConvertToNonLinearBoundingBoxToLinearArea(BoundingBox boundingBox, LinearUnitTypes outputUnits)
        {
            double ret = -1;
            try
            {
                double areaInMeters = 0;
                double conversionFactor = 0;
                if (boundingBox.CoordinateUnits.GetType().Equals(typeof(DecimalDegrees)))
                {
                    //double rise = GreatCircleDistance.LinearDistanceInMeters(boundingBox.BottomLeft, boundingBox.TopLeft);
                    //double run = GreatCircleDistance.LinearDistanceInMeters(boundingBox.BottomLeft, boundingBox.BottomRight);

                    double rise = GreatCircleDistance.LinearDistance2(boundingBox.BottomLeft.Y, boundingBox.BottomLeft.X, boundingBox.TopLeft.Y, boundingBox.TopLeft.X, outputUnits);
                    double run = GreatCircleDistance.LinearDistance2(boundingBox.BottomLeft.Y, boundingBox.BottomLeft.X, boundingBox.BottomRight.Y, boundingBox.BottomRight.X, outputUnits);

                    if (rise == 0 && run != 0)
                    {
                        rise = 1;
                    }
                    else if (rise != 0 && run == 0)
                    {
                        run = 1;
                    }

                    areaInMeters = rise * run;

                    conversionFactor = UnitConverter.GetAreaConversionFactorFromMeters(outputUnits);
                }
                else
                {
                    throw new Exception("Unsupported nonlinear unit input type was encountered: input: " + boundingBox.CoordinateUnits.GetType());
                }

                ret = areaInMeters * conversionFactor;
            }
            catch (Exception e)
            {
                throw new Exception("An Exception occurred in ConvertToNonLinearBoundingBoxToLinearArea ", e);
            }

            return ret;
        }

    }
}


