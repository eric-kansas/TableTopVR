using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace VRStandardAssets.Utils
{
    // This class simply allows the user to return to the main menu.
    public class ReturnToMainMenu : MonoBehaviour
    {
		[SerializeField] private Transform m_startDestination;   // The name of the main menu scene.
		[SerializeField] private Transform m_endDestination;   // The name of the main menu scene.
		[SerializeField] private float time = 1.0f;   // The name of the main menu scene.
        [SerializeField] private VRInput m_VRInput;                     // Reference to the VRInput in order to know when Cancel is pressed.
		private bool m_goingToStartLocation = false;

        private void OnEnable ()
        {
            m_VRInput.OnCancel += HandleCancel;
        }


        private void OnDisable ()
        {
            m_VRInput.OnCancel -= HandleCancel;
        }


        private void HandleCancel ()
        {
			Debug.Log("handel cancel kansas");
            StartCoroutine (MoveHand ());
        }


		private IEnumerator MoveHand ()
        {
			float elapsedTime = 0.0f;

			Vector3 startingPosition = transform.localPosition;
			Quaternion startingRotation = transform.rotation; // have a startingRotation as well

			Vector3 targetPosition = m_goingToStartLocation ? m_startDestination.position : m_endDestination.position;
			Quaternion targetRotation = m_goingToStartLocation ? m_startDestination.rotation : m_endDestination.rotation;
			m_goingToStartLocation = !m_goingToStartLocation;

			while (elapsedTime < time)
			{
				elapsedTime += Time.deltaTime; // <- move elapsedTime increment here
				transform.localPosition = Vector3.Lerp (startingPosition, targetPosition, (elapsedTime / time)   );  
				// Rotations
				transform.rotation = Quaternion.Slerp(startingRotation, targetRotation,  (elapsedTime / time)  );
				yield return new WaitForEndOfFrame ();
			}
        }
    }
}