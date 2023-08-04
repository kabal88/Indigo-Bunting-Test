using System;
using Identifier;
using UnityEngine;

namespace Views
{
    public class AnimationEventProvider : MonoBehaviour
    {
        public event Action<int> AnimationEvent; 

        public void SendAnimationEvent(AnimationEventIdentifier animationEvent)
        {
            AnimationEvent?.Invoke(animationEvent.Id);
        }
    }
}