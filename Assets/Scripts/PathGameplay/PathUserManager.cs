using System.Numerics;

namespace PathGameplay
{
    public enum UserType
    {
        Player = 0,
        Enemy = 1
    }
    
    public class PathUserManager
    {
        private PathGameplay.Path _path;
        
        public PathUserManager(PathGameplay.Path path)
        {
            _path = path;
        }

        public UnityEngine.Vector3 GetSpawnPoint(UserType userType)
        {
            return userType == UserType.Player ? _path.GetFirstPoint() : _path.GetLastPoint();
        }
    }
}