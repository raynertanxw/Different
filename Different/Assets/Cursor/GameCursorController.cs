using UnityEngine;
using System.Collections;

public class GameCursorController : MonoBehaviour
{
	private enum CursorState { Normal, Attract, Idle, Repel };

	private CursorState currentCursorState;

	public Texture2D normalCur;

	private void Awake()
	{
		Cursor.SetCursor(normalCur, Vector2.zero, CursorMode.Auto);

		ChangeCursorState(CursorState.Idle);
	}

	private void Update()
	{
		Vector3 newCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newCursorPos.z = 10.0f;
		transform.position = newCursorPos;

		if (GameManager.Instance.GameIsOver)
		{
			ChangeCursorState(CursorState.Normal);
			return;
		}

		// Left Click
		if (Input.GetMouseButton(0))
		{
			ChangeCursorState(CursorState.Attract);
		}
		// Right Click
		else if (Input.GetMouseButton(1))
		{
			ChangeCursorState(CursorState.Repel);
		}
		// No click / Idle
		else
		{
			ChangeCursorState(CursorState.Idle);
		}
	}

	private void ChangeCursorState(CursorState _curState)
	{
		if (currentCursorState == _curState)
			return;

		switch (_curState)
		{
		case CursorState.Normal:
			break;
		case CursorState.Attract:
			break;
		case CursorState.Idle:
			break;
		case CursorState.Repel:
			break;
		}

		currentCursorState = _curState;
	}
}
