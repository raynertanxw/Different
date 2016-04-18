using UnityEngine;
using System.Collections;

public class GameCursorController : MonoBehaviour
{
	private enum CursorState { Normal, Attract, Idle, Repel };

	private SpriteRenderer normalCursor, attractCursor, idleCursor, repelCursor;
	private CursorState currentCursorState;

	private void Awake()
	{
		Cursor.visible = false;

		normalCursor 	= transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		attractCursor 	= transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
		idleCursor 		= transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
		repelCursor 	= transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();

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
			normalCursor.enabled 	= true;
			attractCursor.enabled 	= false;
			idleCursor.enabled 		= false;
			repelCursor.enabled 	= false;
			break;
		case CursorState.Attract:
			normalCursor.enabled 	= false;
			attractCursor.enabled 	= true;
			idleCursor.enabled 		= false;
			repelCursor.enabled 	= false;
			break;
		case CursorState.Idle:
			normalCursor.enabled 	= false;
			attractCursor.enabled 	= false;
			idleCursor.enabled 		= true;
			repelCursor.enabled 	= false;
			break;
		case CursorState.Repel:
			normalCursor.enabled 	= false;
			attractCursor.enabled 	= false;
			idleCursor.enabled 		= false;
			repelCursor.enabled 	= true;
			break;
		}

		currentCursorState = _curState;
	}
}
