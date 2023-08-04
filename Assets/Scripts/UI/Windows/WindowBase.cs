using Identifier;
using UnityEngine;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private WindowIdentifiers _id;
        
        public int ID => _id.Id;
        
        public abstract void Show();
        public abstract void Hide();
    }
}