using UnityEngine;
using UnityEngine.EventSystems;

namespace Scrips.PBuilding
{
    public class BuildingManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private BuildingTypeListSO _buildingTypeList;
        private BuildingTypeSO _buildingType;

        private void Awake()
        {
            _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
            _buildingType = _buildingTypeList.Items[BuildingTypeId.WoodHarvester];
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _buildingType = _buildingTypeList.Items[BuildingTypeId.WoodHarvester];
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _buildingType = _buildingTypeList.Items[BuildingTypeId.StoneHarvester];
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _buildingType = _buildingTypeList.Items[BuildingTypeId.GoldHarvester];
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(_buildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
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
            _buildingType = _buildingTypeList.Items[buildingTypeId];
        }
    }
}