using System;
using System.Linq;
using System.Windows.Media.Media3D;

namespace RecoCuboid_Sample.Maths
{
    internal static class Calculations
    {
        public static Vector3D IntersectLineWithPlane(Vector3D lineDir, Vector3D linePoint, Vector3D planePoint, Vector3D planeNormal)
        {
            var diff = linePoint - planePoint;
            var prod1 = Vector3D.DotProduct(diff, planeNormal);
            var prod2 = Vector3D.DotProduct(lineDir, planeNormal);
            var prod3 = prod1 / prod2;
            return linePoint - (lineDir * prod3);
        }

        public static Vector3D[] ComputeCubeCordinates(Vector3D cubeCenter, double cubeSideLength)
        {
            Vector3D[] cubeVertices = new Vector3D[8];
            //Numbering style:
            //3 -- 2
            //0 -- 1

            var center = cubeCenter;
            var halfSideLength = cubeSideLength / 2;
            cubeVertices[0] = center + halfSideLength * new Vector3D(0, -1, -1);
            cubeVertices[1] = center + halfSideLength * new Vector3D(0, 1, -1);
            cubeVertices[2] = center + halfSideLength * new Vector3D(0, 1, 1);
            cubeVertices[3] = center + halfSideLength * new Vector3D(0, -1, 1);

            cubeVertices[4] = center + halfSideLength * new Vector3D(0, -1, -1);
            cubeVertices[5] = center + halfSideLength * new Vector3D(0, 1, -1);
            cubeVertices[6] = center + halfSideLength * new Vector3D(0, 1, 1);
            cubeVertices[7] = center + halfSideLength * new Vector3D(0, -1, 1);

            return cubeVertices;
        }

        public static Vector3D[] ProjectCubeCordinates(Vector3D[] originalVertices, Vector3D planePos, Vector3D planeNormal, bool projectBack = false)
        {
            Vector3D[] projectedPoints = new Vector3D[originalVertices.Length];

            for (int i = 0; i < originalVertices.Length; i++)
            {
                var dir = projectBack ? -1 : 1;
                var orig = dir * originalVertices[i];
                var lineDir = orig.GetNormalized();
                var lineVec = orig;

                projectedPoints[i] = IntersectLineWithPlane(lineDir, lineVec,
                    planePos, planeNormal);
            }
            return projectedPoints;
        }

        public static Point3D ComputeCubeCenter(Vector3D[] cubeVertices)
        {
            if (cubeVertices.Length != 8) throw new ArgumentException("only 8 vertices supported");

            return new Point3D(
                   cubeVertices.Sum(p => p.X) / cubeVertices.Length,
                   cubeVertices.Sum(p => p.Y) / cubeVertices.Length,
                   cubeVertices.Sum(p => p.Z) / cubeVertices.Length);
        }

        public static double ComputeCubeSideLength(Vector3D[] cubeVertices)
        {
            var projectedPoints = cubeVertices;
            return ((projectedPoints[6] - projectedPoints[0])).Z;
        }

        public static double Magnitude(this Vector3D vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        public static Vector3D GetNormalized(this Vector3D vector)
        {
            return Vector3D.Divide(vector, vector.Magnitude());
        }

        public static Point3D ToPoint(this Vector3D vector)
        {
            return new Point3D(vector.X, vector.Y, vector.Z);
        }

        public static Vector3D ToVector(this Point3D point)
        {
            return new Vector3D(point.X, point.Y, point.Z);
        }
    }
}