using TMPro;
using UnityEngine;

namespace Views
{
    public class BlockNumberTextHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;

        public void SetNumber(int value)
        {
            _text.text = value.ToString();
        }
    }
}