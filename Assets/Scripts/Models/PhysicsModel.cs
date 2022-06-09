using Assets.Scripts.Interfaces;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.Models
{
    public class PhysicsModel : AbstractPhysicsModel
    {
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Transform checkTransform;
        [SerializeField]
        private float speed = 100f;
        [SerializeField]
        private float stopingRadius = 1.1f;
        private Vector3? distance = null;

        private readonly Logger logger = new();

        override public bool Move(Vector3 path)
        {
            if (distance == null)
            {
                distance = (path - transform.position).normalized;
                distance = new Vector3(distance.Value.x, 0, distance.Value.z);
            }
            if (Vector3.Distance(checkTransform.position, path) < stopingRadius)
            {
                distance = null;
                return true;
            }
            rb.velocity = distance.Value * speed;
            logger.WriteLog("PhysicsModel : Movement gameObject");
            return false;
        }

        override public void Put()
        {
            rb.velocity = Vector3.zero;
        }
    }
}