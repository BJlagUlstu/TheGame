using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    internal class GameObjectShadow: MonoBehaviour
    {
        [SerializeField]
        private Material defaultMaterial;
        [SerializeField]
        private Material errorMaterial;
        [SerializeField]
        private Transform transformConroller;
        private MeshRenderer meshRenderer;
        private int countOfCollisions = 0;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IGameObject>() != null)
            {
                UpdateMeshRenderer(true);
                countOfCollisions++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<IGameObject>() != null)
            {
                countOfCollisions--;
                if (countOfCollisions <= 0) UpdateMeshRenderer(false);
            }
        }

        public void UpdatePosition(Vector3 position)
        {
            transformConroller.position = position;
        }

        public void SetActive(bool active)
        {
            if (!active)
            {
                countOfCollisions = 0;
                UpdateMeshRenderer(false);
            }
            gameObject.SetActive(active);
        }

        void UpdateMeshRenderer(bool status)
        {
            if (status) meshRenderer.material = errorMaterial;
            else meshRenderer.material = defaultMaterial;
        }

        public bool CheckErrorStatus()
        {
            return countOfCollisions <= 0;
        }
    }
}