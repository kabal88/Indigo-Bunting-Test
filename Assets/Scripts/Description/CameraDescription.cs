using System;
using Controllers;
using Identifier;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Description
{
    [Serializable]
    public class CameraDescription : ICameraDescription
    {
        [SerializeField] private CameraIdentifier _id;

        [SerializeField, Range(0,1)] private float _movingRatio;

        public int Id => _id.Id;

        public CameraModel Model => new(_movingRatio);
    }
}