using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DaburuTools;
using DaburuTools.Action;

public abstract class ObstacleController : MonoBehaviour
{
    protected static List<ObstacleController> obstacleControllers = null;
    protected static int mnNumAliveObstacles;
    public static int NumObstacles { get { return obstacleControllers.Count; } }
    public static int NumAliveObstacles { get { return mnNumAliveObstacles; } }

    protected Collider2D thisCol2D;
    protected SpriteRenderer thisSpriteRen;
    protected bool mbEnabled;

    private void Awake()
    {
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

                //TEMPORARY RANDOM DIRECTION
                spawningObstacle.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

                //Scale Animation
                GraphScaleToAction scaleAct = new GraphScaleToAction(spawningObstacle.transform, Graph.InverseExponential, new Vector3(4, 4, 4), 0.5f);
                ActionHandler.RunAction(scaleAct);

                spawningObstacle.OnSpawn();

                return spawningObstacle;
            }
        }

        // HOPE THIS NEVER HAPPENS. IF IT DOES, EXPAND POOL
        Debug.LogWarning("Not enough Obstacles, EXPAND POOL!!!");
        return null;
    }

    protected abstract void OnSpawn();

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
