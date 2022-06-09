using UnityEngine;

namespace Assets
{
    class CameraController : MonoBehaviour
    {
        public float panSpeed = 20f;
        public float panBorderThickness = 20f;

        public Vector2 panLimit;
        public float maxYLimit;
        public float minYLimit;

        public float scrollSpeed = 2000f;

        private Vector3 lastPosition;

        private void Start()
        {
            lastPosition = transform.position;
        }

        void Update()
        {
            Vector3 newPosition = transform.position;

            bool flag = false;

            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                newPosition.z += panSpeed * Time.deltaTime * transform.forward.z;
                newPosition.x += panSpeed * Time.deltaTime * transform.forward.x;
                flag = true;
            }
            if (Input.mousePosition.y <= panBorderThickness)
            {
                newPosition.z -= panSpeed * Time.deltaTime * transform.forward.z;
                newPosition.x -= panSpeed * Time.deltaTime * transform.forward.x;
                flag = true;
            }
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                newPosition.z += panSpeed * Time.deltaTime * transform.right.z;
                newPosition.x += panSpeed * Time.deltaTime * transform.right.x;
                flag = true;
            }
            if (Input.mousePosition.x <= panBorderThickness)
            {
                newPosition.z -= panSpeed * Time.deltaTime * transform.right.z;
                newPosition.x -= panSpeed * Time.deltaTime * transform.right.x;
                flag = true;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (flag || scroll != 0)
            {
                newPosition.y -= scroll * scrollSpeed * Time.deltaTime;

                newPosition.y = Mathf.Clamp(newPosition.y, minYLimit, maxYLimit);
                newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.y, panLimit.y);
                newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);

                lastPosition = newPosition;
            }

            Vector3 position = Vector3.Lerp(transform.position, lastPosition, 0.5f);

            transform.position = position;
        }
    }
}