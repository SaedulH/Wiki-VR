using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hands{

[RequireComponent(typeof(Animator))]   
    public class XRHand : MonoBehaviour
    {
        public float speed;

        [SerializeField]
        private Animator animator;
        private float triggerTarget= 100;
        private float gripTarget = 100;
        private float triggerCurrent;
        private float gripCurrent;
                
        void Update()
        {
            //AnimateHand();
        }

        internal void SetGrip(float val)
        {
            gripTarget = val;
        }

        internal void SetTrigger(float val)
        {
            triggerTarget = val;
        }

        void AnimateHand()
        {
            if(gripCurrent != gripTarget)
            {
                gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
                animator.SetFloat("Grip", gripCurrent);
            }else
            {
                Debug.Log("gripTarget met");
            }
            
            if(triggerCurrent != triggerTarget)
            {
                triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
                animator.SetFloat("Trigger", triggerCurrent);
            }else
            {
                Debug.Log("triggerTarget met");
            }
        }
    }
}