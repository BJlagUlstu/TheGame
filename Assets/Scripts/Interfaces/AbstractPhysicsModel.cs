using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public abstract class AbstractPhysicsModel: MonoBehaviour
    {
        abstract public void Put();
        abstract public bool Move(Vector3 path);
    }
}