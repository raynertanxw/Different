using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DaburuTools;
using DaburuTools.Action;

public class ButtonController : MonoBehaviour
{
	public GameDifficulty difficulty;
	public bool IsLevelButton = false;

	private RectTransform thisRectTransform;
	private RectTransform selectionBoxRectTransform;
	private Vector2 mvecOriginAchorPos;
	private Vector2 mvecButtonDownOffset = new Vector2(0.0f, -20.0f);

	private Vector2 mvecOriginSizeDelta;
	private Vector2 mvecButtonDownSizeDelta = new Vector2(440.0f, 160.0f);

    private void Awake()
    {
		thisRectTransform = gameObject.GetComponent<RectTransform>();
		mvecOriginAchorPos = thisRectTransform.anchoredPosition;
		selectionBoxRectTransform = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
		mvecOriginSizeDelta = selectionBoxRectTransform.sizeDelta;
		
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

	public void SetButtonDown(bool _buttonDown)
	{
		if (_buttonDown)
		{
			thisRectTransform.anchoredPosition = mvecOriginAchorPos + mvecButtonDownOffset;
			selectionBoxRectTransform.sizeDelta = mvecButtonDownSizeDelta;
		}
		else
		{
			thisRectTransform.anchoredPosition = mvecOriginAchorPos;
			selectionBoxRectTransform.sizeDelta = mvecOriginSizeDelta;
		}
	}
}
