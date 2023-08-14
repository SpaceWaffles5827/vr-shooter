using System.Diagnostics;
using Unity.XR.CoreUtils;

namespace UnityEngine.XR.Interaction.Toolkit
{
    /// <summary>
    /// Controls a <see cref="CharacterController"/> height
    /// upon locomotion events of a <see cref="LocomotionProvider"/>.
    /// </summary>
    [AddComponentMenu("XR/Locomotion/Character Controller Driver", 11)]
    public partial class CharacterControllerDriver : MonoBehaviour
    {
        XROrigin m_XROrigin;
        /// <summary>
        /// (Read Only) The <see cref="XROrigin"/> used for driving the <see cref="CharacterController"/>.
        /// </summary>
        protected XROrigin xrOrigin => m_XROrigin;

        CharacterController m_CharacterController;
        /// <summary>
        /// (Read Only) The <see cref="CharacterController"/> that this class drives.
        /// </summary>
        protected CharacterController characterController => m_CharacterController;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Update()
        {
            UpdateCharacterController();
        }

        /// <summary>
        /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
        /// based on the camera's position.
        /// </summary>
        protected virtual void UpdateCharacterController()
        {
            if (m_XROrigin == null || m_CharacterController == null)
                return;

            var height = Mathf.Clamp(m_XROrigin.CameraInOriginSpaceHeight, 1, 2);

            Vector3 center = m_XROrigin.CameraInOriginSpacePos;
            center.y = height / 2f + m_CharacterController.skinWidth;

            m_CharacterController.height = height;
            m_CharacterController.center = center;
        }

        void Subscribe(LocomotionProvider provider)
        {
            if (provider != null)
            {
                provider.beginLocomotion += OnBeginLocomotion;
                provider.endLocomotion += OnEndLocomotion;
            }
        }

        void Unsubscribe(LocomotionProvider provider)
        {
            if (provider != null)
            {
                provider.beginLocomotion -= OnBeginLocomotion;
                provider.endLocomotion -= OnEndLocomotion;
            }
        }

        void OnBeginLocomotion(LocomotionSystem system)
        {
            UpdateCharacterController();
        }

        void OnEndLocomotion(LocomotionSystem system)
        {
            UpdateCharacterController();
        }
    }
}
