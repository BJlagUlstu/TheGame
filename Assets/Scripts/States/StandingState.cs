using Assets.Scripts.Interfaces;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.States
{
    public class StandingState : State
    {
        private readonly Logger logger = new();

        public StandingState(IGameObject gameObject, StateMachine stateMachine, AbstractPhysicsModel physicsModel, AbstractLogicModel logicModel) : base(gameObject, stateMachine, physicsModel, logicModel) { }

        public override void Enter()
        {
            base.Enter();
            physicsModel.Put();
            logger.WriteLog("AttackingState : Enter state");
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.IsSelected())
            {
                stateMachine.ChangeState(new MovingState(gameObject, stateMachine, physicsModel, logicModel));
                logger.WriteLog("AttackingState : Change state on moving");
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            physicsModel.Put();
        }
    }
}