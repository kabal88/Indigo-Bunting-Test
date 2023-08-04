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

        [FormerlySerializedAs("_speed")] [SerializeField] private float _forwardSpeed;
        [HorizontalGroup(LabelWidth = 80)][SerializeField] private float _leftBorder;
        [HorizontalGroup(LabelWidth = 80)][SerializeField] private float _rightBorder;
        [SerializeField] private float _sideSpeed;

        public int Id => _id.Id;

        public CameraModel Model => new(_forwardSpeed,
            _leftBorder,
            _rightBorder,
            _sideSpeed);
    }
}