using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

namespace RecoCuboid_Sample.Maths
{
    internal static class Transformations
    {
        public static Matrix3D ToVCS(this MachineCordinateSystem mcs)
        {
            var matrix = Matrix3D.Identity;
            matrix.Translate(-1 * mcs.DetectorPlanePosition);
            return matrix;
        }

        public static Matrix3D ToMCS(this ViewCordinateSystem vcs)
        {
            var viewMatrix = vcs.MCS.ToVCS();
            viewMatrix.Inverse(); // inverse of vcs is mcs
            return viewMatrix;
        }
    }
}