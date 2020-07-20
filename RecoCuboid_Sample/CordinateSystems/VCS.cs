using HelixToolkit.Wpf;
using System.ComponentModel;
using System.Windows.Media.Media3D;

namespace RecoCuboid_Sample.Maths
{
    class ViewCordinateSystem : INotifyPropertyChanged
    {
        private Point3D _cameraPosition;
        private Vector3D _lookDirection;

        public ViewCordinateSystem()
        {
            MCS = new MachineCordinateSystem();
        }

        public Point3D CameraPosition
        {
            get
            {
                if(_cameraPosition.DistanceTo(new Point3D())==0)
                {
                    return Point3D.Multiply(MCS.XRaySource, MCS.ToVCS());
                }
                return _cameraPosition;
            }
            set
            {
                _cameraPosition = value;
            }
        }

        public Vector3D LookDirection
        {
            get
            {
                if(_lookDirection.Length == 0)
                {
                    return -1 * MCS.DetectorPlaneNormal;
                }
                return _lookDirection;
            }
            set
            {
                _lookDirection = value;
            }
        }

        public Point3D ViewCubeCenter
        {
            get
            {
                return Point3D.Multiply(MCS.ProjectedCubeCenter, MCS.ToVCS());
            }
        }

        public double ViewCubeLength
        {
            get
            {
                return MCS.ProjectedCubeSideLength;
            }

            set
            {
                ModifyViewCubeLength(value);

                OnPropertyChanged("ViewCubeCenter");
                OnPropertyChanged("ViewCubeLength");
            }
        }

        private void ModifyViewCubeLength(double newValue)
        {
            var newCornerPoints = Calculations.ComputeCubeCordinates(MCS.ProjectedCubeCenter.ToVector3D(), newValue);
            var hypotheticalPlanePos = MCS.HypotheticalCubeCenter;
            var hypotheticalPlaneNormal = -1 * MCS.DetectorPlaneNormal;
            var hypotheticalCubePoints = Calculations.ProjectCubeCordinates(newCornerPoints, hypotheticalPlanePos, hypotheticalPlaneNormal, true);
            MCS.ModifyCubeSideLenghts(hypotheticalCubePoints, newCornerPoints);
        }

        public MachineCordinateSystem MCS { get; private set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}