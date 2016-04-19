using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameDifficulty { Easy, Medium, Hard };

public class GameManager : MonoBehaviour
{
	private static GameDifficulty difficulty;
	public static void SetGameDifficulty(GameDifficulty _difficulty)
	{
		difficulty = _difficulty;
	}


	private static GameManager sInstance = null;
	public static GameManager Instance { get { return sInstance; } }

	private bool mbGameIsOver;
	public bool GameIsOver { get { return mbGameIsOver; } }

	private CanvasGroup endGameCG;

	private void Awake()
	{
		if (sInstance == null)
		{
			sInstance = this;
		}
		else if (sInstance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		SetUp();
	}

	private void SetUp()
	{
		mbGameIsOver = false;

		endGameCG = GameObject.Find("EndGamePanel").GetComponent<CanvasGroup>();
		SetEndGamePanelVisbility(false);

		// Spawn starting number of Plebs.
		for (int i = 0; i < 10; i++)
		{
			PlebController.Spawn((Vector3) Random.insideUnitCircle);
        }

        for (int i = 0; i < 5; i++)
        {
            ObstacleController.Spawn((Vector3)Random.insideUnitCircle + Vector3.right * 5);
        }
	}

	private void SetEndGamePanelVisbility(bool _visible)
	{
		if (_visible)
		{
			endGameCG.alpha = 1.0f;
			endGameCG.interactable = true;
			endGameCG.blocksRaycasts = true;
		}
		else
		{
			endGameCG.alpha = 0.0f;
			endGameCG.interactable = false;
			endGameCG.blocksRaycasts = false;
		}
	}

	public void CheckGameOver()
	{
		if (PlebController.NumAlivePlebs <= 0)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		mbGameIsOver = true;



		PresentGameOver();
	}

	private void PresentGameOver()
	{
		SetEndGamePanelVisbility(true);
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
