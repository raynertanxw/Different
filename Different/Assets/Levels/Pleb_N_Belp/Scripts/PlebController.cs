using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DaburuTools.Action;
using DaburuTools;

public class PlebController : MonoBehaviour
{
	private static List<PlebController> PlebControllers = null;
	private static float ForceMultiplier = 50.0f;
	private static int mnNumAlivePlebs;
	public static int NumPlebs { get { return PlebControllers.Count; } }
	public static int NumAlivePlebs { get { return mnNumAlivePlebs; } }

	private Rigidbody2D thisRB;
	private Collider2D thisCol2D;
	private SpriteRenderer thisSpriteRen;
	private bool mbEnabled;

	private void Awake()
	{
		thisRB = gameObject.GetComponent<Rigidbody2D>();
		thisCol2D = gameObject.GetComponent<Collider2D>();
		thisSpriteRen = gameObject.GetComponent<SpriteRenderer>();
//		SetRandColor();

		SetEnabled(false);

		if (PlebControllers == null)
		{
			PlebControllers = new List<PlebController>();
			mnNumAlivePlebs = 0;
		}

		PlebControllers.Add(this);
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == Constants.kTagObstacle)
        {
            ReturnToPool();
			GameManager.Instance.CheckGameOver();
        }
    }

	private void OnDestroy()
	{
		PlebControllers.Remove(this);
		if (PlebControllers.Count == 0)
		{
			PlebControllers = null;
		}
	}

	#region Pool Spawn Controls
	public static PlebController Spawn(Vector3 _spawnPos)
	{
		for (int i = 0; i < PlebControllers.Count; i++)
		{
			if (PlebControllers[i].mbEnabled == false)
			{
				PlebController spawningPleb = PlebControllers[i];
				spawningPleb.transform.position = _spawnPos;
				spawningPleb.SetEnabled(true);
				mnNumAlivePlebs++;

				// Pulse Animation
				PulseAction pulseAct = new PulseAction(spawningPleb.transform, 1,
					Graph.Exponential, Graph.InverseExponential,
					0.1f, 0.5f,
					Vector3.one, new Vector3(2.0f, 2.0f, 2.0f));
				ActionHandler.RunAction(pulseAct);
				AudioManager.Instance.PlayReproductionSound();

				return spawningPleb;
			}
		}

		// HOPE THIS NEVER HAPPENS. IF IT DOES, EXPAND POOL
		Debug.LogWarning("Not enough Plebs, EXPAND POOL!!!");
		return null;
	}

	public void ReturnToPool()
	{
		SetEnabled(false);
		mnNumAlivePlebs--;
		AudioManager.Instance.PlayDeathSound();
	}

	private void SetEnabled(bool _enabled)
	{
		if (_enabled)
		{
			thisRB.isKinematic = false;
			thisCol2D.enabled = true;
			thisSpriteRen.enabled = true;
		}
		else
		{
			thisRB.isKinematic = true;
			thisCol2D.enabled = false;
			thisSpriteRen.enabled = false;
		}

		mbEnabled = _enabled;
	}
	#endregion

	private void Update()
	{
		if (!mbEnabled)
			return;

		if (GameManager.Instance.GameIsPaused)
			return;

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

	private void SetRandColor()
	{
		Color newCol = new Color(Random.Range(0.0f, 0.3f), Random.Range(0.0f, 0.3f), Random.Range(0.0f, 0.3f), 1.0f);
		while (newCol.r + newCol.g + newCol.b < 0.3f)
		{
			newCol = new Color(Random.Range(0.0f, 0.3f), Random.Range(0.0f, 0.3f), Random.Range(0.0f, 0.3f), 1.0f);
		}

		thisSpriteRen.color = newCol;
	}
}
