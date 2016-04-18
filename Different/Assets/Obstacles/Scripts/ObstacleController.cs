using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DaburuTools;
using DaburuTools.Action;

public class ObstacleController : MonoBehaviour
{
    private static List<ObstacleController> obstacleControllers = null;
    private static int mnNumAliveObstacles;
    public static int NumObstacles { get { return obstacleControllers.Count; } }
    public static int NumAliveObstacles { get { return mnNumAliveObstacles; } }

    private Rigidbody2D thisRB;
    private Collider2D thisCol2D;
    private SpriteRenderer thisSpriteRen;
    private bool mbEnabled;

    private void Awake()
    {
        thisRB = gameObject.GetComponent<Rigidbody2D>();
        thisCol2D = gameObject.GetComponent<Collider2D>();
        thisSpriteRen = gameObject.GetComponent<SpriteRenderer>();

        SetEnabled(false);

        if (obstacleControllers == null)
        {
            obstacleControllers = new List<ObstacleController>();
            mnNumAliveObstacles = 0;
        }

        obstacleControllers.Add(this);
    }

    private void OnDestroy()
    {
        obstacleControllers.Remove(this);
        if (obstacleControllers.Count == 0)
        {
            obstacleControllers = null;
        }
    }

    #region Pool Spawn Controls
    public static ObstacleController Spawn(Vector3 _spawnPos)
    {
        for (int i = 0; i < obstacleControllers.Count; i++)
        {
            if (obstacleControllers[i].mbEnabled == false)
            {
                ObstacleController spawningObstacle = obstacleControllers[i];
                spawningObstacle.transform.position = _spawnPos;
                spawningObstacle.SetEnabled(true);
                mnNumAliveObstacles++;

                // Pulse Animation
                PulseAction pulseAct = new PulseAction(spawningObstacle.transform, 1,
                    Graph.Exponential, Graph.InverseExponential,
                    0.1f, 0.5f,
                    new Vector3(4, 4, 4), new Vector3(8, 8, 8));
                ActionHandler.RunAction(pulseAct);

                return spawningObstacle;
            }
        }

        // HOPE THIS NEVER HAPPENS. IF IT DOES, EXPAND POOL
        Debug.LogWarning("Not enough Obstacles, EXPAND POOL!!!");
        return null;
    }

    public void ReturnToPool()
    {
        SetEnabled(false);
        mnNumAliveObstacles--;
    }

    private void SetEnabled(bool _enabled)
    {
        if (_enabled)
        {
            thisCol2D.enabled = true;
            thisSpriteRen.enabled = true;
        }
        else
        {
            thisCol2D.enabled = false;
            thisSpriteRen.enabled = false;
        }

        mbEnabled = _enabled;
    }
    #endregion
}
