using System.Collections;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Helpers
{
    internal class Selector: MonoBehaviour
    {
        private GameObjectsManager gameObjectsManager;
        private Vector2 startPosition;
        private Vector2 endPosition;

        private bool selected = false;

        private readonly ArrayList selectedGameObjects = new();
        private readonly ArrayList gameObjects = new();
        private readonly ArrayList spaceSelectedGameObjects = new();

        private readonly Logger logger = new();

        public void Initializing(GameObjectsManager gameObjectsManager)
        {
            this.gameObjectsManager = gameObjectsManager;
        }

        public void StartSelect(Vector2 position)
        {
            selected = true;
            startPosition = position;
            logger.WriteLog("Selector : Selection start");
        }

        public void UpdateSelection(Vector2 position)
        {
            if (selected)
            {
                endPosition = position;
                CheckSelected();
            }
        }

        public void EndSelected()
        {
            selectedGameObjects.AddRange(spaceSelectedGameObjects);
            spaceSelectedGameObjects.Clear();
            endPosition = Vector2.zero;
            startPosition = Vector2.zero;
            selected = false;
            logger.WriteLog("Selector : Selection end");
        }

        public void ClickOnGameObject(GameObject gameObject)
        {
            IGameObject gameObjectC = gameObject.GetComponent<IGameObject>();
            if (gameObjectC.IsSelected())
            {
                gameObjectC.Deselect();
                selectedGameObjects.Remove(gameObjectC);
                gameObjects.Remove(gameObject);
                logger.WriteLog("Selector : Deselecting gameObject by click");
            }
            else if (IsGenericType(selectedGameObjects, gameObjectC))
            {
                gameObjectC.Select();
                selectedGameObjects.Add(gameObjectC);
                gameObjects.Add(gameObject);
                logger.WriteLog("Selector : Selecting gameObject by click");
            }
        }

        private bool IsGenericType(ArrayList objectList, IGameObject gameObject)
        {
            if (objectList.Count != 0)
            {
                var lastObject = objectList.ToArray().Last();
                if (gameObject.GetType() == lastObject.GetType()) return true;
            }
            else
            {
                return true;
            }
            return false;
        }

        public void ClickNearGameObjects()
        {
            foreach (IGameObject gameObject in selectedGameObjects)
            {
                gameObject.Deselect();
            }
            selectedGameObjects.Clear();
            gameObjects.Clear();
            logger.WriteLog("Selector : Deselecting gameObjects by click on non gameObjects");
        }

        private void CheckSelected()
        {
            var camera = Camera.main;
            var viewportsBouns = SelectorUtils.GetViewportBounds(camera, startPosition, Input.mousePosition);
            foreach (GameObject gameObject in gameObjectsManager.GetGameObjects())
            {
                var gameObjectC = gameObject.GetComponentInChildren<IGameObject>();
                var inSpace = viewportsBouns.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
                if (inSpace && !spaceSelectedGameObjects.Contains(gameObjectC) && IsGenericType(spaceSelectedGameObjects, gameObjectC))
                {
                    gameObjectC.Select();
                    gameObjects.Add(gameObject);
                    spaceSelectedGameObjects.Add(gameObjectC);
                    logger.WriteLog("Selector : Selecting gameObjects by rectangle");
                }
                if (!inSpace && spaceSelectedGameObjects.Contains(gameObjectC))
                {
                    gameObjectC.Deselect();
                    spaceSelectedGameObjects.Remove(gameObjectC);
                    gameObjects.Remove(gameObject);
                    logger.WriteLog("Selector : Deselecting gameObjects by rectangle");
                }
            }
        }

        public ArrayList GetSelected()
        {
            return selectedGameObjects;
        }

        public ArrayList GetSelectedGameObjects()
        {
            return gameObjects;
        }

        public void DeselectAll()
        {
            selectedGameObjects.Clear();
        }

        void OnGUI()
        {
            if (selected)
            {
                Rect rect = SelectorUtils.GetScreenRect(startPosition, endPosition);
                SelectorUtils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.3f));
                SelectorUtils.DrawScreenBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            }
        }
    }
}