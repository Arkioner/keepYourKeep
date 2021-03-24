using UnityEngine;
using UnityEngine.EventSystems;

namespace Scrips.PBuilding
{
    public class BuildingManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private BuildingTypeListSO _buildingTypeList;
        private BuildingTypeSO _activeBuildingType;

        private void Awake()
        {
            _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetBuildingType(BuildingTypeId.WoodHarvester);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SetBuildingType(BuildingTypeId.StoneHarvester);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                SetBuildingType(BuildingTypeId.GoldHarvester);
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(_activeBuildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mwp = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mwp.z = 0;
            return mwp;
        }

        public void SetBuildingType(BuildingTypeId buildingTypeId)
        {
            _activeBuildingType = _buildingTypeList.Items[buildingTypeId];
        }
    }
}