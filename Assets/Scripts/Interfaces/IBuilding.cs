using Assets.Scripts.Configs;

namespace Assets.Scripts.Interfaces
{
    internal interface IBuilding: IGameObject
    {
        public void SetConfing(BuildingConfig config);
    }
}