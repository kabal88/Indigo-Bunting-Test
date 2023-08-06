using Data;
using Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

public class Snapshoter : MonoBehaviour
{
    [SerializeField] private Transform _hipBone;
    [SerializeField] private BoneData[] _standUpAnimationStartBoneData;
    [SerializeField] private Animator _animator;

    [Button("Snapshot from anim clip"), BoxGroup("Animation")]
    private void SnapshotStartBoneDataFromAnimationClip(string clipName = "Stand Up")
    {
        var bones = _hipBone.GetComponentsInChildren<Transform>();
        _standUpAnimationStartBoneData = new BoneData[bones.Length];
            
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                clip.SampleAnimation(gameObject, 0);
                _standUpAnimationStartBoneData.SnapshotBoneData(bones);
                Debug.Log("Snapshot done");
                return;
            }
        }
            
        Debug.Log("No clip found");
    }
}
