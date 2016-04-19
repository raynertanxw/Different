using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DaburuTools;
using DaburuTools.Action;

public class ButtonController : MonoBehaviour
{
	public GameDifficulty difficulty;
	public bool IsLevelButton = false;

    private void Awake()
    {
		PulseAction pulseAct = new PulseAction(this.transform, 1, Graph.SmoothStep, 1.0f, Vector3.one, 1.1f * Vector3.one);
		ActionRepeatForever repeatForever = new ActionRepeatForever(pulseAct);
		ActionHandler.RunAction(repeatForever);

		if (!IsLevelButton)
			return;

		Text highscoreText = transform.GetChild(2).gameObject.GetComponent<Text>();

		switch(difficulty)
		{
		case GameDifficulty.Easy:
			if (PlayerPrefs.HasKey(Constants.kEasyHighscoreKey))
				highscoreText.text = Utility.FormatTime(PlayerPrefs.GetFloat(Constants.kEasyHighscoreKey));
			break;
		case GameDifficulty.Medium:
			if (PlayerPrefs.HasKey(Constants.kMediumHighscoreKey))
				highscoreText.text = Utility.FormatTime(PlayerPrefs.GetFloat(Constants.kMediumHighscoreKey));
			break;
		case GameDifficulty.Hard:
			if (PlayerPrefs.HasKey(Constants.kHardHighscoreKey))
				highscoreText.text = Utility.FormatTime(PlayerPrefs.GetFloat(Constants.kHardHighscoreKey));
			break;
		}
    }

    public void DisplaySelection(bool display)
    {
        transform.FindChild("SelectionBox").GetComponent<Image>().enabled = display;
    }
}
