using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
	public void RetryButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitButton()
	{
		SceneManager.LoadScene("Menu-Scene");
	}
}
