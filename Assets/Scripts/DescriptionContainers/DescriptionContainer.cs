using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Descriptions;


namespace DescriptionContainers
{
    public class DescriptionContainer<T> : Descriptions.Description where T : IDescription, new()
    {
        [SerializeField, HideLabel] private T description;

        public override IDescription GetDescription => description;
    }
}