using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageWidget : MonoBehaviour
    {
        protected Image Image;

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        public virtual void SetSprite(Sprite sprite)
        {
            Image.sprite = sprite;
        }
    }
}