using Assets.Scripts;
using Assets.Scripts.Configs;
using Assets.Scripts.Interfaces;
using Assets.Scripts.States;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

public class Character : MonoBehaviour, ICharacter
{
    private IGameObject target;

    [SerializeField]
    private SelectingObject selectingObject;
    private StateMachine stateMachine;
    [SerializeField]
    private AbstractPhysicsModel physicsModel;
    [SerializeField]
    private AbstractLogicModel logicModel;

    private CharacterConfig config;

    private readonly Logger logger = new();

    private bool isSelected = false;
    private bool isNear = false;

    public void Deselect()
    {
        isSelected = false;
        selectingObject.SetVisible(false);
        logger.WriteLog("Character : GameObject deselected");
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
        logger.WriteLog("Character : GameObject destroyed");
    }

    public void Hit()
    {
        target.ToBeHit(config.Damage);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        isSelected = true;
        selectingObject.SetVisible(isSelected);
        logger.WriteLog("Character : GameObject selected");
    }

    public void SetConfig(CharacterConfig config)
    {
        this.config = config;
    }

    public void ToBeHit(int damage)
    {
        config.HealthPoints -= damage;
    }

    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.Initialize(new StandingState(this, stateMachine, physicsModel, logicModel));
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetTarget(IGameObject target)
    {
        this.target = target;
        logger.WriteLog("Character : GameObject set target");
    }

    public IGameObject GetTarget()
    {
        return target;
    }

    public bool TargetIsNear()
    {
        return isNear;
    }

    public bool IsDestroyed()
    {
        return config.HealthPoints <= 0;
    }
}