using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	[SerializeField] Transform Player;
	[SerializeField] float timeOffset;
	[SerializeField] Vector3 offsetPos;

	[SerializeField] Vector3 boundsMin;
	[SerializeField] Vector3 boundsMax;
	
	private void LateUpdate()
	{
		if (Player != null)
		{
			Vector3 startPos = transform.position;
			Vector3 targetPos = Player.position;

			targetPos.x += offsetPos.x;
			targetPos.y += offsetPos.y;
			targetPos.z = transform.position.z;

			targetPos.x = Mathf.Clamp(targetPos.x, boundsMin.x, boundsMax.x);
			targetPos.y = Mathf.Clamp(targetPos.y, boundsMin.y, boundsMax.y);

			float t = 1f - Mathf.Pow(1f - timeOffset, Time.deltaTime * 30);
			transform.position = Vector3.Lerp(startPos,targetPos, t);
		}
	
	}

}
