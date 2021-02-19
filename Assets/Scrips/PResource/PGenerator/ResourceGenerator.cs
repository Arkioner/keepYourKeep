using System.Collections.Generic;
using System.Linq;
using Scrips.PBuilding;
using UnityEngine;

namespace Scrips.PResource.PGenerator
{
    [RequireComponent(typeof(BuildingController))]
    public class ResourceGenerator : MonoBehaviour
    {
        private ResourceGeneratorDataHolder _resourceGeneratorDataHolder = null;
        private IDictionary<ResourceTypeId, Timer> _resourceGenerationTimer = null;

        private void Awake()
        {
            _resourceGeneratorDataHolder = GetComponent<BuildingController>().BuildingTypeSO.ResourceGeneratorDataHolder;
            _resourceGenerationTimer = _resourceGeneratorDataHolder.GetResourceIds
                .Aggregate(
                    new Dictionary<ResourceTypeId, Timer>(),
                    (timers, resourceTypeId) =>
                    {
                        timers.Add(resourceTypeId, InitTimerForResource(resourceTypeId));
                        return timers;
                    }
                );
        }

        void Update()
        {
            foreach (ResourceTypeId resourceTypeId in _resourceGenerationTimer.Keys)
            {
                if (_resourceGenerationTimer[resourceTypeId].IsTime())
                {
                    AddResource(resourceTypeId);
                }
            }
        }

        private void AddResource(ResourceTypeId resourceTypeId)
        {
            ResourceManager.AddResource(resourceTypeId, _resourceGeneratorDataHolder.GetResourceGeneratorData(resourceTypeId).Amount);
        }

        private Timer InitTimerForResource(ResourceTypeId resourceTypeId)
        {
            return new Timer(_resourceGeneratorDataHolder.GetResourceGeneratorData(resourceTypeId).GenerationSpan);
        }
    }
}