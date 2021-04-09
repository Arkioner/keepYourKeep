using System.Collections.Generic;
using System.Linq;
using Scrips;
using Scrips.PBuilding;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] [NotNull] private RectTransform buildingSelectorTemplate = null;
    [SerializeField] private float leftMarginPxl = 0;
    [SerializeField] private float marginBetweenButtonsPxl = 0;
    [SerializeField] private float bottomMarginPxl = 0;
    [SerializeField] private float btnHeight = 100;
    [SerializeField] private float btnWidth = 100;
    [SerializeField] [NotNull] private BuildingManager buildingManager;
    private Dictionary<BuildingTypeId, Transform> _buildingSelectorUIDictionary;

    private void Awake()
    {
        BuildingTypeListSO resourceTypeListSO = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buildingSelectorUIDictionary = resourceTypeListSO.Items.Select((resource, index) =>
        {
            RectTransform buildingSelectorBtn = Instantiate(buildingSelectorTemplate, transform);
            SetPositionOfButton(index, buildingSelectorBtn);
            buildingSelectorBtn.Find("Image").GetComponent<Image>().sprite = resource.Value.Icon;
            buildingSelectorBtn.GetComponent<Button>().onClick.AddListener(() => SelectBuildingToBuild(resource.Value.Id));
            buildingSelectorBtn.Find("SelectedFrame/CancelSelectionBtn").GetComponent<Button>().onClick.AddListener(() => UnselectBuildingToBuild(resource.Value.Id));
            return new KeyValuePair<BuildingTypeId, Transform>(resource.Key, buildingSelectorBtn);
        }).ToDictionary(row => row.Key, row => row.Value);
    }

    private void SetPositionOfButton(int index, RectTransform buildingSelectorBtn)
    {
        buildingSelectorBtn.sizeDelta = new Vector2(btnWidth, btnHeight);
        buildingSelectorBtn.anchoredPosition = new Vector2(btnWidth / 2 + leftMarginPxl + (marginBetweenButtonsPxl + btnWidth) * index, bottomMarginPxl + btnHeight / 2);
    }

    private void SelectBuildingToBuild(BuildingTypeId activeBuildingTypeId)
    {
        buildingManager.SetActiveBuildingType(activeBuildingTypeId);
        foreach ((BuildingTypeId buildingTypeId, Transform button) in _buildingSelectorUIDictionary)
        {
            button.Find("SelectedFrame").gameObject.SetActive(buildingTypeId.Equals(activeBuildingTypeId));
        }
    }
    
    private void UnselectBuildingToBuild(BuildingTypeId activeBuildingTypeId)
    {
        buildingManager.UnsetActiveBuildingType();
        _buildingSelectorUIDictionary[activeBuildingTypeId].Find("SelectedFrame").gameObject.SetActive(false);
    }
}