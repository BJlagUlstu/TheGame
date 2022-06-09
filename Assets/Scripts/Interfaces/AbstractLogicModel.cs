using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public abstract class AbstractLogicModel: MonoBehaviour
    {
        abstract public Vector3[] CalcultePath();
    }
}