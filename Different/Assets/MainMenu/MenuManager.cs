using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DaburuTools;
using DaburuTools.Action;

public class MenuManager : MonoBehaviour
{
    public GameObject[] canvases;

	private void Awake()
	{
		AudioManager.Instance.StartMenuMusic();

		if (PlayerPrefs.HasKey(Constants.kBGMVolumeKey))
		{
			GameObject BGMSlider = GameObject.Find("BGM Slider");
			if (BGMSlider != null)
			{
				BGMSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Constants.kBGMVolumeKey);
			}
		}
		if (PlayerPrefs.HasKey(Constants.kSFXVolumeKey))
		{
			GameObject SFXSlider = GameObject.Find("SFX Slider");
			if (SFXSlider != null)
			{
				SFXSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Constants.kSFXVolumeKey);
			}
		}
	}

	public void ChangeBGMVolume(float _newVolume)
	{
		AudioManager.Instance.ChangeBGMVolume(_newVolume);
	}

	public void ChangeSFXVolume(float _newVolume)
	{
		AudioManager.Instance.ChangeSFXVolume(_newVolume);
	}

    public void LoadLevel(int _diff)
    {
		AudioManager.Instance.PlayClickSound();
        GameManager.SetGameDifficulty((GameDifficulty)_diff);
        SceneManager.LoadScene("Main-Scene");
    }

    public void SlideUp()
    {
        Slide(Vector2.up);
    }

    public void SlideDown()
    {
        Slide(Vector2.down);
    }

    public void SlideLeft()
    {
        Slide(Vector2.left);
    }

    public void SlideRight()
    {
        Slide(Vector2.right);
    }

    private void Slide(Vector2 _direction)
    {
		AudioManager.Instance.PlayClickSound();
        _direction = new Vector2(Mathf.Round(Mathf.Clamp(_direction.x, -1, 1)), Mathf.Round(Mathf.Clamp(_direction.y, -1, 1)));
        float width = 19.2f, height = 10.8f;
        foreach (GameObject canvas in canvases)
        {
            //canvas.transform.localPosition += new Vector3(width * _direction.x, height * _direction.y, 0);
            GraphLocalMoveByAction localMove = new GraphLocalMoveByAction(canvas.transform, Graph.SmoothStep, new Vector3(width * _direction.x, height * _direction.y, 0), 0.5f);
            ActionHandler.RunAction(localMove);
        }
    }

	public void ExitGame()
	{
		Application.Quit();
	}
}
