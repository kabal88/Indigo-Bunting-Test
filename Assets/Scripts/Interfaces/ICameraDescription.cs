using Controllers;

namespace Interfaces
{
    public interface ICameraDescription : IDescription
    {
        public CameraModel Model { get; }
    }
}