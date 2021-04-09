using UnityEngine;

namespace Scrips
{
    public static class UIUtils
    {
        private static Camera _mainCamera;

        public static Vector3 GetMouseWorldPosition()
        {
            if (_mainCamera == null) _mainCamera = Camera.main;
            Vector3 mwp = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mwp.z = 0;
            return mwp;
        }
    }
}