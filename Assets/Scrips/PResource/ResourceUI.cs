using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scrips.PResource
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] [NotNull] private Transform resourceTemplate = null;
        [SerializeField] private float resourceLeftMarginPxl = -120;
        private Dictionary<ResourceTypeId, Transform> _resourceVisorUIDictionary;

        private void Awake()
        {
            ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
            _resourceVisorUIDictionary = resourceTypeListSO.Items.Select((resource, index) =>
            {
                Transform resourceVisor = Instantiate(resourceTemplate, transform);
                resourceVisor.GetComponent<RectTransform>().anchoredPosition = new Vector2(resourceLeftMarginPxl * index, 0);
                resourceVisor.Find("Image").GetComponent<Image>().sprite = resource.Icon;
                return new KeyValuePair<ResourceTypeId, Transform>(resource.Id, resourceVisor);
            }).ToDictionary(row => row.Key, row => row.Value);
        }

        private void Start()
        {
            ResourceManager.ResourceAmountChanged += ResourceManagerOnResourceAmountChanged;
            UpdateResources();
        }

        private void ResourceManagerOnResourceAmountChanged(object sender, EventArgs e)
        {
            UpdateResources();
        }

        private void UpdateResources()
        {
            foreach ((ResourceTypeId resourceTypeId, Transform resourceVisor) in _resourceVisorUIDictionary)
            {
                resourceVisor.Find("Text").GetComponent<TextMeshProUGUI>().SetText(ResourceManager.GetResourceAmount(resourceTypeId).ToString().PadLeft(4, '0'));
            }
        }
    }
}