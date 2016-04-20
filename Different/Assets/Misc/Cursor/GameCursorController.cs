using UnityEngine;
using System.Collections;

public class GameCursorController : MonoBehaviour
{
	private enum CursorState { Normal, Attract, Idle, Repel };

	private CursorState currentCursorState;

	public Texture2D normalCurTex, attractCurTex, idleCurTex, repelCurTex;
	private Vector2 normalCurVec, mfmCurVec;
	private CursorMode autoCursorMode = CursorMode.Auto;

	private void Awake()
	{
		normalCurVec = Vector2.zero;
		mfmCurVec = new Vector2(16.0f, 16.0f);

		ChangeCursorState(CursorState.Idle);
	}

	private void Update()
	{
		if (GameManager.Instance == null)
		{
			ChangeCursorState(CursorState.Normal);
			return;
		}

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
			Cursor.SetCursor(normalCurTex, normalCurVec, autoCursorMode);
			break;
		case CursorState.Attract:
			Cursor.SetCursor(attractCurTex, mfmCurVec, autoCursorMode);
			break;
		case CursorState.Idle:
			Cursor.SetCursor(idleCurTex, mfmCurVec, autoCursorMode);
			break;
		case CursorState.Repel:
			Cursor.SetCursor(repelCurTex, mfmCurVec, autoCursorMode);
			break;
		}

		currentCursorState = _curState;
	}
}
