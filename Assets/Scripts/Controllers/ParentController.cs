using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts.Controllers
{
    internal class ParentController : MonoBehaviour
    {
        [SerializeField]
        private AbstractController workController;
        [SerializeField]
        private AbstractController buildingController;
        [SerializeField]
        private AbstractController characterBuildController;
        [SerializeField]
        private AbstractController defaultController;
        [SerializeField]
        private Camera _camera;

        private readonly Logger logger = new();

        private void Start()
        {
            workController.PrepareToWork();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && workController.Click())
            {
                UpdateController(ControllerAction.SIMPLE_CLICK);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && workController.ClickUp())
            {
                UpdateController(ControllerAction.SIMPLE_CLICKUP);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UpdateController(ControllerAction.SIMPLE_CLICK);
            }
            workController.ObserveMousePosition();
        }

        public void OnMenuBuildingClick()
        {
            UpdateController(ControllerAction.MENU_BUILDING_CLICK);
            logger.WriteLog("ParentController : Clicking on building menu item");
        }

        public void OnMenuCharacterClick()
        {
            UpdateController(ControllerAction.MENU_CHARACTER_CLICK);
            logger.WriteLog("ParentController : Clicking on character menu item");
        }

        private void UpdateController(ControllerAction controllerAction)
        {
            switch (controllerAction)
            {
                case ControllerAction.MENU_BUILDING_CLICK:
                    SwapWorkController(buildingController);
                    break;
                case ControllerAction.SIMPLE_CLICK:
                    SwapWorkController(defaultController);
                    break;
                case ControllerAction.SIMPLE_CLICKUP:
                    SwapWorkController(defaultController);
                    break;
                case ControllerAction.MENU_CHARACTER_CLICK:
                    SwapWorkController(characterBuildController);
                    break;
            }
        }

        private void SwapWorkController(AbstractController newController)
        {
            if (workController == newController) return;
            workController.StopWork();
            workController = newController;
            workController.PrepareToWork();
            logger.WriteLog("ParentController : Swapping controller");
        }

        enum ControllerAction
        { 
            MENU_BUILDING_CLICK, SIMPLE_CLICK, SIMPLE_CLICKUP, MENU_CHARACTER_CLICK
        }
    }
}