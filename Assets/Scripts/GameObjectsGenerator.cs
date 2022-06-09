using UnityEngine;

using Logger = Assets.Scripts.Helpers.Logger;

namespace Assets.Scripts
{
    internal class GameObjectsGenerator: MonoBehaviour
    {
        [SerializeField]
        private GameObject buildingPrefab;
        [SerializeField]
        private GameObject characterPrefab;

        private readonly Logger logger = new();

        public GameObject CreateBuildingPrefab(Vector3 position)
        {
            logger.WriteLog("GameObjectsGenerator : Creating building prefab");
            return GameObject.Instantiate(buildingPrefab, position, Quaternion.identity);
        }

        public GameObject CreateCharacterPrefab(Vector3 position)
        {
            logger.WriteLog("GameObjectsGenerator : Creating character prefab");
            return GameObject.Instantiate(characterPrefab, position, Quaternion.identity);
        }
    }
}