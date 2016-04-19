using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameDifficulty { Easy, Medium, Hard };

public class GameManager : MonoBehaviour
{
	private static GameDifficulty sDifficulty = GameDifficulty.Easy;
	public static GameDifficulty Difficulty { get { return sDifficulty; } }
	public static void SetGameDifficulty(GameDifficulty _difficulty)
	{
		sDifficulty = _difficulty;
		switch (_difficulty)
		{
		case GameDifficulty.Easy:
			mstrHighscoreKey = Constants.kEasyHighscoreKey;
			break;
		case GameDifficulty.Medium:
			mstrHighscoreKey = Constants.kMediumHighscoreKey;
			break;
		case GameDifficulty.Hard:
			mstrHighscoreKey = Constants.kHardHighscoreKey;
			break;
		}
	}
	public GameObject EasyLevelPrefab, MediumLevelPrefab, HardLevelPrefab;


	private static GameManager sInstance = null;
	public static GameManager Instance { get { return sInstance; } }

	private bool mbGameIsOver;
	public bool GameIsOver { get { return mbGameIsOver; } }

	private float mfTimeSurvived = 0.0f;
	private Text timerText, highscoreText;
	private static string mstrHighscoreKey;

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

		// Survival time text
		mfTimeSurvived = 0.0f;
		timerText = GameObject.Find("TimerText").GetComponent<Text>();
		timerText.text = "00:00:00";

		// Highscore text
		highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
		if (PlayerPrefs.HasKey(mstrHighscoreKey))
			highscoreText.text = "Best - " + Utility.FormatTime(PlayerPrefs.GetFloat(mstrHighscoreKey));
		else
			highscoreText.text = "";

		// Spawn the Level Prefab
		switch (sDifficulty)
		{
		case GameDifficulty.Easy:
			GameObject.Instantiate(EasyLevelPrefab, Vector3.zero, Quaternion.identity);
			break;
		case GameDifficulty.Medium:
			GameObject.Instantiate(MediumLevelPrefab, Vector3.zero, Quaternion.identity);
			break;
		case GameDifficulty.Hard:
			GameObject.Instantiate(HardLevelPrefab, Vector3.zero, Quaternion.identity);
			break;
		}

		// Set BGM
		AudioManager.Instance.FadeOutMenuMusic();
		AudioManager.Instance.StartBGM(sDifficulty);

		// Spawn starting number of Plebs.
		for (int i = 0; i < 10; i++)
		{
			PlebController.Spawn((Vector3) Random.insideUnitCircle);
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

		CheckForHighScore();

		PresentGameOver();
		AudioManager.Instance.FadeOutBGM();
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
		if (!mbGameIsOver)
		{
			mfTimeSurvived += Time.deltaTime;
			timerText.text = Utility.FormatTime(mfTimeSurvived);
		}
	}

	private void CheckForHighScore()
	{
		if (PlayerPrefs.HasKey(mstrHighscoreKey) == false)
		{
			PlayerPrefs.SetFloat(mstrHighscoreKey, mfTimeSurvived);
			highscoreText.text = "New Best Survival Time!";
			highscoreText.color = Color.green;

			return;
		}
		else
		{
			float oldHighscore = PlayerPrefs.GetFloat(mstrHighscoreKey);

			if (mfTimeSurvived > oldHighscore)
			{
				PlayerPrefs.SetFloat(mstrHighscoreKey, mfTimeSurvived);
				highscoreText.text = "New Best Survival Time!";
				highscoreText.color = Color.green;
			}
		}
	}
}
