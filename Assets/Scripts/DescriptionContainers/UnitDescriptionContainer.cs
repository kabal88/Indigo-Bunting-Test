using Descriptions;
using UnityEngine;

namespace DescriptionContainers
{
    [CreateAssetMenu(fileName = "UnitDescription", menuName = "Descriptions/Unit Description",
        order = 0)]
    public class UnitDescriptionContainer : DescriptionContainer<UnitDescription>
    {
    }
}