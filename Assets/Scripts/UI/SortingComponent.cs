using UnityEngine;

namespace UI
{
    public class SortingComponent : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void SetSorting(int order)
        {
            _canvas.sortingOrder = order;
        }
    }
}