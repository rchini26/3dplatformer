using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public enum AnimationType
    {
        None,
        Idle,
        Run,
        Attack,
        Death
    }
    
    public class AnimationBase : MonoBehaviour
    {
        public List<AnimationSetup> animationSetups;
        public Animator animator;
        
        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetups.Find(i => i.animationType == animationType);
            if (setup != null)
            {
                animator.SetTrigger(setup.animationTrigger);
            }
        }
    } 
    
    [System.Serializable]
    public class AnimationSetup

    {
    public AnimationType animationType;
    public string animationTrigger;
    }
}
