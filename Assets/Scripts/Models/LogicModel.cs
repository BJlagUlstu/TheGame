using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.Models
{
    public class LogicModel : AbstractLogicModel
    {
        [SerializeField]
        private NavMeshAgent agent;
        private NavMeshPath path;

        private readonly Logger logger = new();

        void Start()
        {
            path = new NavMeshPath();
        }

        override public Vector3[] CalcultePath()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3[] pathDefault = new Vector3[] { };
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                agent.gameObject.SetActive(true);
                agent.CalculatePath(targetPosition, path);
                agent.gameObject.SetActive(false);
                logger.WriteLog("LogicModel : Calculate path is ready");

                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    logger.WriteLog("LogicModel : Path status is complete");
                    return path.corners;
                }
            }
            return pathDefault;
        }
    }
}