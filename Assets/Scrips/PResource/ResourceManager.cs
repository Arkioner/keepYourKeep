using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scrips.PResource
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager _instance;
        private Dictionary<ResourceTypeId, int> _resourceAmountDictionary;

        public static event EventHandler ResourceAmountChanged;
        private void Awake()
        {
            _instance = this;
            _resourceAmountDictionary = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO))
                .Items
                .Aggregate(
                    new Dictionary<ResourceTypeId, int>(),
                    (dic, rt) =>
                    {
                        dic.Add(rt.Id, 0);
                        return dic;
                    });
        }

        public static void AddResource(ResourceTypeId resourceTypeId, int amount)
        {
            _instance._resourceAmountDictionary[resourceTypeId] += amount;
            //TODO: May include the resources updated in the event?
            ResourceAmountChanged?.Invoke(_instance, EventArgs.Empty);
        }

        public static int GetResourceAmount(ResourceTypeId resourceTypeId)
        {
            return _instance._resourceAmountDictionary[resourceTypeId];
        }
    }
}