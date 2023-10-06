using System.Linq;

namespace PathGameplay
{
    public enum UserType
    {
        Player = 0,
        Enemy = 1
    }
    
    public class PathUserManager
    {
        private UserType _userType;
        private PathGameplay.Path _path;
        private int _index;
        
        public PathUserManager(PathGameplay.Path path, UserType user)
        {
            _path = path;
            _userType = user;
            _index = _userType == UserType.Player ? 0 : _path.PathPoints.Count - 1;
        }

        public void SetNewPath(Path path)
        {
            _path = path;
            _index = _userType == UserType.Player ? 0 : _path.PathPoints.Count - 1;
        }

        public UnityEngine.Vector3 GetSpawnPoint(UserType userType)
        {
            _userType = userType;
            return userType == UserType.Player ? _path.GetFirstPoint() : _path.GetLastPoint();
        }

        public UnityEngine.Vector3 GetNextPoint()
        {
            switch (_userType)
            {
                case UserType.Player:
                {
                    if (_index < _path.PathPoints.Count - 1)
                    {
                        _index++;
                    }
                    break;
                }
                case UserType.Enemy:
                {
                    if (_index > 0)
                    {
                        _index--;
                    }
                    break;
                }
            }

            return _path.PathPoints[_index];
        } 
    }
}