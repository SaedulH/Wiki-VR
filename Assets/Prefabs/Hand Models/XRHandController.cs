using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

namespace Hands{

[RequireComponent(typeof(ActionBasedController))]



    public class XRHandController : MonoBehaviour
    {
        [SerializeField]
        private ActionBasedController controller;

        [SerializeField]
        private InputActionReference TriggerReference;

        [SerializeField]
        private InputActionReference GripReference;        
        public XRHand hand;
        
        void Awake()
        {
            //controller = GetComponent<ActionBasedController>();
            TriggerReference.asset.Enable();
            GripReference.asset.Enable();
        }

        void Update()
        {
            hand.SetGrip(GripReference.action.ReadValue<float>());
            hand.SetTrigger(TriggerReference.action.ReadValue<float>());
        }
    }
}