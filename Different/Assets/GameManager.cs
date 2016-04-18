using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager sInstance = null;
	public static GameManager Instance { get { return sInstance; } }

	private void Awake()
	{
		if (sInstance == null)
		{
			sInstance = this;
		}
		else if (sInstance != this)
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		for (int i = 0; i < 30; i++)
		{
			PlebController.Spawn((Vector3) Random.insideUnitCircle);
		}
	}

	private void OnDestroy()
	{
		if (sInstance == this)
		{
			sInstance = null;
		}
	}
}
