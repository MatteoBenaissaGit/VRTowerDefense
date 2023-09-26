using Controllers;
using PathGameplay;

namespace Managers
{
    /// <summary>
    /// Manage the spawning of troops on the field
    /// </summary>
    public class TroopSpawnerManager
    {
        private UserType _user;
        
        public TroopSpawnerManager(UserType user)
        {
            _user = user;
        }

        public TroopController SpawnTroopAtPath(int pathNumber, int numberOfTroops, SoldierType type)
        {
            Path path = GameManager.Instance.Paths[pathNumber];
            TroopController troop = RessourceManager.Instance.InstantiateTroop();
            troop.SpawnTroops(new TroopGameplayData(type,numberOfTroops,_user,path));
            troop.transform.name = $"{type.ToString()} troop {_user.ToString()}";
            return troop;
        }
    }
}