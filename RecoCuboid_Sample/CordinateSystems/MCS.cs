using System.ComponentModel;
using System.Windows.Media.Media3D;

namespace RecoCuboid_Sample.Maths
{
    class MachineCordinateSystem : INotifyPropertyChanged
    {
        public MachineCordinateSystem():
            this(new Point3D(0, 0, 0))
        {
        }

        public MachineCordinateSystem(
            Point3D xRaySource, 
            double detectorPlanePos = 1000, 
            double cubeCenter = 700, 
            double cubeSideLength = 100, 
            double detectorWidth=1000, 
            double detectorHeight=1000 )
        {
            XRaySource = xRaySource;
            DetectorPlanePosition = new Vector3D(detectorPlanePos, 0, 0);
            HypotheticalCubeCenter = new Vector3D(cubeCenter, 0, 0);
            HypotheticalCubeSideLength = cubeSideLength;
            DetectorPlaneWidth = detectorWidth;
            DetectorPlaneHeight = detectorHeight;

            OriginalCubePoints = Calculations.ComputeCubeCordinates(HypotheticalCubeCenter, HypotheticalCubeSideLength);
            ProjectedCubePoints = Calculations.ProjectCubeCordinates(OriginalCubePoints, DetectorPlanePosition, DetectorPlaneNormal);
        }

        private Vector3D[] OriginalCubePoints { get; set; }

        private Vector3D[] ProjectedCubePoints { get; set; }

        public Point3D XRaySource { get; private set; }

        public Vector3D DetectorPlanePosition { get; private set; }

        public Vector3D DetectorPlaneNormal => new Vector3D(-1, 0, 0);

        public double DetectorPlaneWidth { get; }

        public double DetectorPlaneHeight { get; }

        public Vector3D HypotheticalCubeCenter { get; private set; }

        public double HypotheticalCubeSideLength { get; set; }

        public Point3D ProjectedCubeCenter
        {
            get
            {
                return Calculations.ComputeCubeCenter(ProjectedCubePoints);
            }
        }

        public double ProjectedCubeSideLength
        {
            get
            {
                return Calculations.ComputeCubeSideLength(ProjectedCubePoints);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ModifyCubeSideLenghts(Vector3D[] origVecs, Vector3D[] projectedVecs)
        {
            OriginalCubePoints = origVecs;
            ProjectedCubePoints = projectedVecs;
            HypotheticalCubeSideLength = Calculations.ComputeCubeSideLength(OriginalCubePoints);

            OnPropertyChanged("HypotheticalCubeCenter");
            OnPropertyChanged("HypotheticalCubeSideLength");
            OnPropertyChanged("ProjectedCubeSideLength");
            OnPropertyChanged("ProjectedCubeCenter");
        }
    }
}