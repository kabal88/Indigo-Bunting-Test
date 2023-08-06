using Data;
using UnityEngine;

namespace Helpers
{
    public static class Extensions
    {
        public static void SnapshotBoneData(this BoneData[] boneData, Transform[] bones)
        {
            for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
            {
                boneData[boneIndex].Position = bones[boneIndex].localPosition;
                boneData[boneIndex].Rotation = bones[boneIndex].localRotation;
            }
        }
        
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }
    }
}