using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlebController : MonoBehaviour
{
	private Rigidbody2D thisRB;
	private static float ForceMultiplier = 50.0f;

	private void Awake()
	{
		thisRB = gameObject.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		// Left click Attracts
		if (Input.GetMouseButton(0))
		{
			Attract();
		}

		// Right click Repels
		else if (Input.GetMouseButton(1))
		{
			Repel();
		}
	}

	private void Attract()
	{
		thisRB.AddForce(ForceToMFM(), ForceMode2D.Force);
	}

	private void Repel()
	{
		thisRB.AddForce(-ForceToMFM(), ForceMode2D.Force);
	}

	private Vector2 ForceToMFM()
	{
		Vector2 distVec = InputController.Instance.mouseWorldPos - (Vector2)transform.position;
		float proximityForceMultiplier = 1.0f / distVec.sqrMagnitude;
		if (proximityForceMultiplier > 10.0f)
			proximityForceMultiplier = 10.0f;
		distVec *= proximityForceMultiplier;
		return distVec * ForceMultiplier;
	}
}
