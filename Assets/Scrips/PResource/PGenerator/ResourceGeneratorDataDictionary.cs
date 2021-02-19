using System;

namespace Scrips.PResource.PGenerator
{
    [Serializable]
    public class ResourceGeneratorDataDictionary : SerializableDictionary<ResourceTypeId, ResourceGeneratorDataHolder.ResourceGeneratorData>
    {}
}