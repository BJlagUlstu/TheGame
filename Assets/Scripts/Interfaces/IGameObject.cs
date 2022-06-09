
namespace Assets.Scripts.Interfaces
{
    public interface IGameObject
    {
        public void Destroy();
        public bool IsSelected();
        public void Select();
        public void Deselect();
        public void ToBeHit(int damage);
        public bool IsDestroyed();
    }
}