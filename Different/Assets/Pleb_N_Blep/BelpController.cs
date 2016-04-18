using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class BelpController : MonoBehaviour
{
	private Rigidbody2D thisRB;
	private static float ForceMultiplier = 20.0f;

	private void Awake()
	{
		thisRB = gameObject.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		// Left click Repels
		if (Input.GetMouseButton(0))
		{
			Repel();
		}

		// Right click Attracts
		else if (Input.GetMouseButton(1))
		{
			Attract();
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
