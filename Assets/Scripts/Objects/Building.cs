using Assets.Scripts.Configs;
using Assets.Scripts.Interfaces;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

public class Building : MonoBehaviour, IBuilding
{
    [SerializeField]
    private SelectingObject selectingObject;
    private BuildingConfig config;

    private bool isSelected = false;

    private readonly Logger logger = new();

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
        logger.WriteLog("Building : GameObject destroyed");
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        isSelected = true;
        selectingObject.SetVisible(isSelected);
        logger.WriteLog("Building : GameObject selected");
    }

    public void Deselect()
    {
        isSelected = false;
        selectingObject.SetVisible(false);
        logger.WriteLog("Building : GameObject deselected");
    }

    public void SetConfing(BuildingConfig config)
    {
        this.config = config;
    }

    public void ToBeHit(int damage)
    {
        config.HealthPoints -= damage;
    }

    public bool IsDestroyed()
    {
        return config.HealthPoints <= 0;
    }
}