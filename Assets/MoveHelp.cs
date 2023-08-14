using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class MoveHelp : MonoBehaviour
    {
        private XROrigin XROrigin;
        private CharacterController CharacterController;

        public float MinHeight = 0f;
        public float MaxHeight = 99f;

        private void Awake()
        {
            if (!XROrigin)
                XROrigin = GetComponent<XROrigin>();

            if (!CharacterController)
                CharacterController = GetComponent<CharacterController>();
        }


        void Update()
        {
            UpdateCharacterController();
        }

        protected virtual void UpdateCharacterController()
        {
            if (XROrigin == null || CharacterController == null)
                return;

            var height = Mathf.Clamp(XROrigin.CameraInOriginSpaceHeight, MinHeight, MaxHeight);

            Vector3 center = XROrigin.CameraInOriginSpacePos;
            center.y = height / 2f + CharacterController.skinWidth;

            CharacterController.height = height;
            CharacterController.center = center;
        }
    }
}