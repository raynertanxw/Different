using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DaburuTools;
using DaburuTools.Action;

public class MenuManager : MonoBehaviour
{
    public GameObject[] canvases;

    public void LoadLevel(int _diff)
    {
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
        _direction = new Vector2(Mathf.Round(Mathf.Clamp(_direction.x, -1, 1)), Mathf.Round(Mathf.Clamp(_direction.y, -1, 1)));
        float width = 19.2f, height = 10.8f;
        foreach (GameObject canvas in canvases)
        {
            //canvas.transform.localPosition += new Vector3(width * _direction.x, height * _direction.y, 0);
            GraphLocalMoveByAction localMove = new GraphLocalMoveByAction(canvas.transform, Graph.SmoothStep, new Vector3(width * _direction.x, height * _direction.y, 0), 0.5f);
            ActionHandler.RunAction(localMove);
        }
    }
}
