using Assets.Scripts.Helpers;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.Controllers
{
    internal class GameObjectController : AbstractController
    {
        [SerializeField]
        private GameObjectShadow gameObjectShadow;
        [SerializeField]
        private GameObjectsManager gameObjectsManager;
        [SerializeField]
        private LayerMask gameObjectMask;
        [SerializeField]
        private LayerMask groundMask;
        [SerializeField]
        private GameObjectType gameObjectType = GameObjectType.BUILDING;

        private readonly Logger logger = new();

        override public bool Click()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, groundMask) && gameObjectShadow.CheckErrorStatus())
            {
                gameObjectShadow.SetActive(false);
                gameObjectsManager.Create(hitData.point, gameObjectType);
                logger.WriteLog("GameObjectController : Creating");
                endWork = true;
            }
            return false;
        }

        public override bool ClickUp()
        {
            return endWork;
        }

        override public bool ObserveMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, groundMask))
            {
                gameObjectShadow.UpdatePosition(hitData.point);
            }
            return false;
        }

        override public void PrepareToWork()
        {
            base.PrepareToWork();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, groundMask))
            {
                gameObjectShadow.gameObject.SetActive(true);
                gameObjectShadow.UpdatePosition(hitData.point);
            }
        }

        override public void StopWork()
        {
            gameObjectShadow.SetActive(false);
        }
    }

    enum GameObjectType 
    { 
        BUILDING, CHARACTER
    }
}