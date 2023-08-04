using Descriptions;
using UnityEngine;

namespace DescriptionContainers
{
    [CreateAssetMenu(fileName = "GameDescription", menuName = "Descriptions/Game Description", order = 0)]
    public class GameDescriptionContainer : DescriptionContainer<GameDescription>
    {
    }
}