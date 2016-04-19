using UnityEngine;
using System.Collections;

public class CameraResizer : MonoBehaviour
{
	// Resolution during design of game. This was the 4S coordinate space for iOS ver of game.
	public int DesignTimeScreenWidth = 1920;
	public int DesignTimeScreenHeight = 1080;

	private float DesignTimeAspectRatio;
	private float DesignTimeOrthographicSize;

	// World space min and max as seen by the camera.
	public static float minX, maxX, minY, maxY;

	void Awake()
	{
		DesignTimeOrthographicSize = Camera.main.GetComponent<Camera>().orthographicSize;
		DesignTimeAspectRatio = (float)DesignTimeScreenWidth / (float)DesignTimeScreenHeight;

		// Calculate the current aspect ratio of device screen.
		float aspectRatio = (float)Screen.width / (float)Screen.height;

		// This gives the correct camera size to maintain a fixed width.
		float size = DesignTimeOrthographicSize / (aspectRatio/DesignTimeAspectRatio);
		Camera.main.GetComponent<Camera>().orthographicSize = size;

		// ???
		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);

		// Get the corners of the camera and convert to world space.
		Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
		Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

		// Cache the corners into min and max values.
		minX = bottomCorner.x;
		minY = bottomCorner.y;
		maxX = topCorner.x;
		maxY = topCorner.y;
	}	
}
