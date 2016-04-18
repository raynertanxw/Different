using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	private static InputController sInstance = null;
	public static InputController Instance { get { return sInstance; } }

	public Vector2 mouseWorldPos = Vector2.zero;

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

	private void Update()
	{
		mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnDestroy()
	{
		if (sInstance == this)
			sInstance = null;
	}
}
