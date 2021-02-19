using Cinemachine;
using UnityEngine;

namespace Scrips.PCamera
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 30f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float zoomStep = 2f;
        [SerializeField] [NotNull] private CinemachineVirtualCamera virtualCamera = null;
        [SerializeField] [Range(10, 30)] private float minOrthographicSize = 10;
        [SerializeField] [Range(30, 50)] private float maxOrthographicSize = 30;

        private float orthographicSize;

        private void Start()
        {
            orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        }

        private void Update()
        {
            UpdateCameraPosition();
            UpdateCameraZoom();
        }

        private void UpdateCameraPosition()
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            Vector3 cameraMovement = new Vector3(xInput, yInput);
            if (!cameraMovement.Equals(Vector3.zero))
            {
                transform.position += cameraMovement * (cameraSpeed * Time.deltaTime);
            }
        }

        private void UpdateCameraZoom()
        {
            float scrollInput = -Input.mouseScrollDelta.y;
            if (scrollInput != 0)
            {
                float originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
                float targetOrthographicSize = originalOrthographicSize + scrollInput * zoomStep;
                targetOrthographicSize = Mathf.Lerp(originalOrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
                targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
                virtualCamera.m_Lens.OrthographicSize = targetOrthographicSize;
            }
        }
    }
}