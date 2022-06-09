using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public abstract class State
    {
        protected IGameObject gameObject;

        protected StateMachine stateMachine;
        protected AbstractLogicModel logicModel;
        protected AbstractPhysicsModel physicsModel;

        protected State(IGameObject gameObject, StateMachine stateMachine, AbstractPhysicsModel physicsModel, AbstractLogicModel logicModel)
        {
            this.gameObject = gameObject;
            this.stateMachine = stateMachine;
            this.logicModel = logicModel;
            this.physicsModel = physicsModel;
        }

        public virtual void Enter() { }
        public virtual void HandleInput() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
}