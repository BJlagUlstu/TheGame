using Assets.Scripts.Interfaces;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.States
{
    public class AttackingState : State
    {
        private readonly Logger logger = new();

        public AttackingState(IGameObject gameObject, StateMachine stateMachine, AbstractPhysicsModel physicsModel, AbstractLogicModel logicModel) : base(gameObject, stateMachine, physicsModel, logicModel) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (gameObject.IsDestroyed())
            {
                stateMachine.ChangeState(new DestroyedState(gameObject, stateMachine, physicsModel, logicModel));
                logger.WriteLog("AttackingState : Change state on destroyed");
            }
            if (gameObject is Character character)
            {
                character.Hit();
                logger.WriteLog("AttackingState : Character create hit");
            }
        }
    }
}