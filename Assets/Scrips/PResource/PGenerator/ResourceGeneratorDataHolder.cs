using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scrips.PResource.PGenerator
{
    [Serializable]
    public class ResourceGeneratorDataHolder
    {
        [SerializeField] private ResourceGeneratorDataDictionary resourcesData = null;

        public List<ResourceTypeId> GetResourceIds => resourcesData.Keys.ToList();

        public ResourceGeneratorData GetResourceGeneratorData(ResourceTypeId resourceTypeId) => resourcesData[resourceTypeId];

        [Serializable]
        public class ResourceGeneratorData
        {
            [SerializeField] [Min(0)] private int amount = 0;
            [SerializeField] [Min(0.1f)] private float generationSpan = 1f;

            public int Amount => amount;
            public float GenerationSpan => generationSpan;
        }
    }
}