using Scrips.PResource.PGenerator;
using UnityEditor;

namespace Scrips.Editor
{
    [CustomPropertyDrawer(typeof(ResourceGeneratorDataDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}