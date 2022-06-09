using Assets.Scripts.Configs;

namespace Assets.Scripts.Interfaces
{
    internal interface ICharacter: IGameObject
    {
        public void SetConfig(CharacterConfig config);
        public void Hit();
        public void SetTarget(IGameObject target);
        public bool TargetIsNear();
    }
}