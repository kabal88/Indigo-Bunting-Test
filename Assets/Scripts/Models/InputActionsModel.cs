using System.Collections.Generic;
using Descriptions;
using Helpers;
using UnityEngine.InputSystem;

namespace Models
{
    public class InputActionsModel
    {
        public List<UpdateableAction> UpdateableActions;
        
        private InputActionAsset _actions;

        public InputActionAsset Actions => _actions;

        public ReadOnlyList<InputActionSettings> InputActionSettings;

        public InputActionsModel(InputActionAsset actions, List<InputActionSettings> inputActionSettings)
        {
            _actions = actions;
            InputActionSettings = new ReadOnlyList<InputActionSettings>(inputActionSettings);
            UpdateableActions = new List<UpdateableAction>();
        }
        
        public bool TryGetInputAction(string name, out InputAction inputAction)
        {
            foreach (var a in _actions.actionMaps)
            {
                foreach (var action in a.actions)
                {
                    if (action.name == name)
                    {
                        inputAction = action;
                        return true;
                    }
                }
            }

            inputAction = null;
            return false;
        }
    }
}