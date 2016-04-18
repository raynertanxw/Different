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
			
		for (int i = 0; i < 10; i++)
		{
			PlebController.Spawn((Vector3) Random.insideUnitCircle);
        }

        for (int i = 0; i < 5; i++)
        {
            ObstacleController.Spawn((Vector3)Random.insideUnitCircle + Vector3.right * 5);
        }
	}

	private void OnDestroy()
	{
		if (sInstance == this)
		{
			sInstance = null;
		}
	}

	private void Update()
	{
		
	}
}
