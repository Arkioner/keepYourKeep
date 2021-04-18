using System;
using System.Linq;
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
                Vector3 mouseWorldPosition = UIUtils.GetMouseWorldPosition();
                if (CanBuildHere(_activeBuildingType, mouseWorldPosition))
                {
                    Instantiate(_activeBuildingType.Prefab, mouseWorldPosition, Quaternion.identity);
                }
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

        private bool CanBuildHere(BuildingTypeSO buildingType, Vector3 position)
        {
            BoxCollider2D boxCollider2D = buildingType.Prefab.GetComponent<BoxCollider2D>();
            return Physics2D.OverlapBoxAll(position + (Vector3) boxCollider2D.offset, boxCollider2D.size, 0).Length == 0;
        }
    }
}