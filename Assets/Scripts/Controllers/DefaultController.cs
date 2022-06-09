using Assets.Scripts.Helpers;
using Assets.Scripts.Interfaces;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.Controllers
{
    internal class DefaultController : AbstractController
    {
        [SerializeField]
        private Selector selector;
        [SerializeField]
        private LayerMask gameObjectMask;
        [SerializeField]
        private GameObjectsManager gameObjectsManager;

        private readonly Logger logger = new();

        private void Start()
        {
            selector.Initializing(gameObjectsManager);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                gameObjectsManager.RemoveGameObjects(selector.GetSelectedGameObjects());
                foreach (IGameObject gameObject in selector.GetSelected())
                {
                    gameObject.Destroy();
                }
                selector.DeselectAll();
            }
        }

        public override bool Click()
        {
            selector.StartSelect(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, gameObjectMask))
            {
                selector.ClickOnGameObject(hitData.transform.gameObject);
                logger.WriteLog("DefaultController : Clicking on object");
            }
            else
            {
                selector.ClickNearGameObjects();
                logger.WriteLog("DefaultController : Clicking near object");
            }
            return false;
        }

        public override bool ClickUp()
        {
            selector.EndSelected();
            logger.WriteLog("DefaultController : ClickUp");
            return false;
        }

        public override bool ObserveMousePosition()
        {
            selector.UpdateSelection(Input.mousePosition);
            return false;
        }

        public override void PrepareToWork() { }

        public override void StopWork()
        {
            selector.EndSelected();
        }
    }
}