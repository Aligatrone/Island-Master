using UnityEngine;

namespace _Scripts.CameraCore
{
	public class CameraPivotIsometric : MonoBehaviour
	{
		public float targetAngle = 45f;
		public float currentAngle;
		public float rotationSpeed = 5f;

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Q))
				targetAngle += 45;

			if(Input.GetKeyDown(KeyCode.E))
				targetAngle -= 45;

			if(targetAngle < 0)
				targetAngle += 360;

			if(targetAngle > 360)
				targetAngle -= 360;

			currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
			transform.rotation = Quaternion.Euler(30, currentAngle, 0);
		}
	}
}