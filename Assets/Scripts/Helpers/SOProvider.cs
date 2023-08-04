using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

#pragma warning disable
namespace Helpers
{
    /// <summary>
    ///This helper gather all ScriptableObjects of needed type and return IEnumerable, its useful for drop down menus
    /// </summary>
    /// <returns></returns>
    public class SOProvider<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// this is editor only code,  dont run it runtime
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetCollection()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
Debug.LogError("dont use soProvider runtime");
#endif

#if UNITY_EDITOR
            var containers = AssetDatabase.FindAssets($"t: {typeof(T).Name}")
            .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
            .Select(x => UnityEditor.AssetDatabase.LoadAssetAtPath<T>(x)).ToList();

            return containers;
#endif

            return default;
        }
    }
}