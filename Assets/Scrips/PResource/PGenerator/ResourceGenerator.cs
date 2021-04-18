using System;
using System.Collections.Generic;
using System.Linq;
using Scrips.PBuilding;
using Scrips.PResource.PNode;
using UnityEngine;

namespace Scrips.PResource.PGenerator
{
    [RequireComponent(typeof(BuildingController))]
    public class ResourceGenerator : MonoBehaviour
    {
        private ResourceGeneratorDataHolder _resourceGeneratorDataHolder = null;
        private IDictionary<ResourceTypeId, Timer> _resourceGenerationTimer = null;
        private IDictionary<ResourceTypeId, int> _resourceAmountGeneratedXTick = null;

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

        private void Start()
        {
            _resourceAmountGeneratedXTick = Physics2D.OverlapCircleAll(transform.position, 5f)
                .ToList()
                .Select(item => item.GetComponent<ResourceNode>())
                .Where(item => item != null)
                .SelectMany(resourceNode => resourceNode.AvailableResources.Intersect(_resourceGeneratorDataHolder.GetResourceIds))
                .GroupBy(id => id, id => id)
                .ToDictionary(
                    ids => ids.Key,
                    ids => ids.Count(id => true) * _resourceGeneratorDataHolder.GetResourceGeneratorData(ids.Key).Amount);
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
            if (_resourceAmountGeneratedXTick.ContainsKey(resourceTypeId))
            {
                ResourceManager.AddResource(resourceTypeId, _resourceAmountGeneratedXTick[resourceTypeId]);
            }
        }

        private Timer InitTimerForResource(ResourceTypeId resourceTypeId)
        {
            return new Timer(_resourceGeneratorDataHolder.GetResourceGeneratorData(resourceTypeId).GenerationSpan);
        }
    }
}