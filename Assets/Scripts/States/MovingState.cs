using Assets.Scripts.Interfaces;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.States
{
    public class MovingState : State
    {
        private Vector3[] path;
        private int currentPathIndex = 0;

        private readonly Logger logger = new();

        public MovingState(IGameObject gameObject, StateMachine stateMachine, AbstractPhysicsModel physicsModel, AbstractLogicModel logicModel) : base(gameObject, stateMachine, physicsModel, logicModel) { }

        public override void Enter()
        {
            base.Enter();
            path = logicModel.CalcultePath();
            logger.WriteLog("MovingState : Enter state");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (path.Length == 0)
            {
                stateMachine.ChangeState(new StandingState(gameObject, stateMachine, physicsModel, logicModel));
                logger.WriteLog("MovingState : Change state on standing by LogicUpdate");
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (physicsModel.Move(path[currentPathIndex]))
            {
                currentPathIndex++;
                if (currentPathIndex == path.Length)
                {
                    stateMachine.ChangeState(new StandingState(gameObject, stateMachine, physicsModel, logicModel));
                    logger.WriteLog("MovingState : Change state on standing by PhysicsUpdate");
                }
            }
        }
    }
}