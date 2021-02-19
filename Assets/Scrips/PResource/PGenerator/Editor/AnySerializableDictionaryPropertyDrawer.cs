using UnityEditor;

namespace Scrips.PResource.PGenerator.Editor
{
    [CustomPropertyDrawer(typeof(ResourceGeneratorDataDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}