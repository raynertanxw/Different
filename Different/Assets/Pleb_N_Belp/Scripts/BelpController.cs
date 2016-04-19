using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class BelpController : MonoBehaviour
{
	private Rigidbody2D thisRB;
	private static float ForceMultiplier = 20.0f;

	private float mfCooldownTimer = 0.0f;
	private const float kReproductionCooldown = 1.0f;

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

		if (mfCooldownTimer > 0.0f)
			mfCooldownTimer -= Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == Constants.kTagObstacle)
		{
			GameManager.Instance.GameOver();
			Destroy(this.gameObject);
		}

		if (mfCooldownTimer > 0.0f)
			return;

		if (col.gameObject.tag == Constants.kTagPleb)
		{
			if (PlebController.NumAlivePlebs < PlebController.NumPlebs)
			{
				PlebController.Spawn(col.contacts[0].point);
				mfCooldownTimer = kReproductionCooldown;
			}
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
