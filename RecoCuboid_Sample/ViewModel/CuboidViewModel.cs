using RecoCuboid_Sample.Maths;

namespace RecoCuboid_Sample
{
    internal class CuboidViewModel : BaseViewModel
    {
        public CuboidViewModel()
        {
            VCS = new ViewCordinateSystem();
            MCS = VCS.MCS;
        }

        public ViewCordinateSystem VCS { get; }

        public MachineCordinateSystem MCS { get; }

        public override string ToString()
        {
            return "Cuboid";
        }
    }
}