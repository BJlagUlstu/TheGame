using Assets.Scripts.Interfaces;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.States
{
    public class DestroyedState : State
    {
        private readonly Logger logger = new();

        public DestroyedState(IGameObject gameObject, StateMachine stateMachine, AbstractPhysicsModel physicsModel, AbstractLogicModel logicModel) : base(gameObject, stateMachine, physicsModel, logicModel) { }

        public override void Enter()
        {
            base.Enter();
            gameObject.Destroy();
            logger.WriteLog("DestroyedState : Enter state");
        }
    }
}