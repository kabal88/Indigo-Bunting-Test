using System;
using Interfaces;

[Serializable]
public class HideOnTouchAbility : IAbility
{
    public void Execute(IOwner owner = null, ITarget target = null)
    {
        if (owner == null)
            return;
        
        owner.Transform.gameObject.SetActive(false);
    }
}
