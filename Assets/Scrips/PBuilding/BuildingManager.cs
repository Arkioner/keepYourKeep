using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scrips.PBuilding
{
    public class BuildingManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private BuildingTypeListSO _buildingTypeList;
        private BuildingTypeSO _activeBuildingType;
        private bool HasSelectedBuildingToBuild => !(_activeBuildingType is null);

        public static event EventHandler<OnActiveBuildingTypeChangedEvent> OnActiveBuildingTypeChanged;

        public class OnActiveBuildingTypeChangedEvent : EventArgs
        {
            public readonly BuildingTypeSO activeBuildingType;

            public OnActiveBuildingTypeChangedEvent(BuildingTypeSO activeBuildingType)
            {
                this.activeBuildingType = activeBuildingType;
            }
        }

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
                SetActiveBuildingType(BuildingTypeId.WoodHarvester);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SetActiveBuildingType(BuildingTypeId.StoneHarvester);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                SetActiveBuildingType(BuildingTypeId.GoldHarvester);
            }

            if (HasSelectedBuildingToBuild && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(_activeBuildingType.Prefab, UIUtils.GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        public void SetActiveBuildingType(BuildingTypeId buildingTypeId)
        {
            _activeBuildingType = _buildingTypeList.Items[buildingTypeId];
            OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEvent(_activeBuildingType));
        }

        public void UnsetActiveBuildingType()
        {
            _activeBuildingType = null;
            OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEvent(null));
        }
    }
}