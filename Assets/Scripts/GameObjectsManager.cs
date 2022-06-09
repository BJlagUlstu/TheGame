using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts
{
    internal class GameObjectsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObjectsGenerator gameObjectsGenerator;
        private ArrayList gameObjects;

        private readonly Logger logger = new();

        public ArrayList GetGameObjects()
        {
            return gameObjects;
        }

        private void Start()
        {
            gameObjects = new ArrayList();
        }

        public void RemoveGameObjects(ArrayList gameObjectsRemove)
        {
            foreach (var gameObject in gameObjectsRemove)
            {
                gameObjects.Remove(gameObject);
            }
        }

        private GameObject CreateBuilding(Vector3 position)
        {
            var gameObject = gameObjectsGenerator.CreateBuildingPrefab(position);
            IGameObject abstractObject = gameObject.GetComponentInChildren<IGameObject>();
            (abstractObject as Building).SetConfing(new(100));
            GetGameObjects().Add(gameObject);
            logger.WriteLog("GameObjectsManager : Creating building");
            return gameObject;
        }

        private GameObject CreateCharacter(Vector3 position)
        {
            var gameObject = gameObjectsGenerator.CreateCharacterPrefab(position);
            IGameObject abstractObject = gameObject.GetComponentInChildren<IGameObject>();
            (abstractObject as Character).SetConfig(new(100, 5));
            GetGameObjects().Add(gameObject);
            logger.WriteLog("GameObjectsManager : Creating character");
            return gameObject;
        }

        public GameObject Create(Vector3 position, GameObjectType gameObjectType)
        {
            switch (gameObjectType)
            {
                case GameObjectType.BUILDING:
                    return CreateBuilding(position);
                case GameObjectType.CHARACTER:
                    return CreateCharacter(position);
                default:
                    break;
            }
            return null;
        }
    }
}