using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
	public void RetryButton()
	{
		AudioManager.Instance.PlayClickSound();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitButton()
	{
		AudioManager.Instance.PlayClickSound();
		SceneManager.LoadScene("Menu-Scene");
	}
}
