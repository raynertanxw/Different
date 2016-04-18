using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {

    }
    
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
