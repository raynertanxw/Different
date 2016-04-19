using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DaburuTools;
using DaburuTools.Action;

public abstract class ObstacleController : MonoBehaviour
{
    protected static List<List<ObstacleController>> obstacleControllers = null;
    protected static List<ObstacleController> spikeWallControllers = null;
    protected static List<ObstacleController> spearControllers = null;
    protected static List<ObstacleController> swordControllers = null;
    protected static int mnNumAliveObstacles;
    public static int NumObstacles { get { return obstacleControllers.Count; } }
    public static int NumAliveObstacles { get { return mnNumAliveObstacles; } }

    protected Collider2D thisCol2D;
    protected SpriteRenderer thisSpriteRen;
    protected bool mbEnabled;

    protected virtual void Awake()
    {
        thisCol2D = gameObject.GetComponent<Collider2D>();
        thisSpriteRen = gameObject.GetComponent<SpriteRenderer>();

        SetEnabled(false);

        if (obstacleControllers == null)
        {
            obstacleControllers = new List<List<ObstacleController>>();
            for (int i = 0; i < 3; i++)
            {
                obstacleControllers.Add(new List<ObstacleController>());
            }
            mnNumAliveObstacles = 0;
        }
        else if (obstacleControllers.Count < 3)
        {
            for (int i = 0; i < 3 - obstacleControllers.Count; i++)
            {
                obstacleControllers.Add(new List<ObstacleController>());
            }
        }

        spikeWallControllers = obstacleControllers[0];
        spearControllers = obstacleControllers[1];
        swordControllers = obstacleControllers[2];
    }

    protected virtual void OnDestroy()
    {
        if (obstacleControllers.Count == 0)
        {
            obstacleControllers = null;
        }
    }

    #region Pool Spawn Controls
    protected static ObstacleController SpawnBase(List<ObstacleController> _controllers, Vector3 _spawnPos, float _spawnDir, float _speedFactor = -1)
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i].mbEnabled == false)
            {
                ObstacleController spawningObstacle = _controllers[i];
                spawningObstacle.transform.position = _spawnPos;
                spawningObstacle.transform.localEulerAngles = new Vector3(0, 0, _spawnDir);
                spawningObstacle.SetEnabled(true);
                mnNumAliveObstacles++;

                //Scale Animation
                Vector3 scale = spawningObstacle.transform.localScale;
                spawningObstacle.transform.localScale = Vector3.zero;
                GraphScaleToAction scaleAct = new GraphScaleToAction(spawningObstacle.transform, Graph.InverseExponential, scale, 0.5f);
                ActionHandler.RunAction(scaleAct);

                spawningObstacle.OnSpawn(_speedFactor);

                return spawningObstacle;
            }
        }

        // HOPE THIS NEVER HAPPENS. IF IT DOES, EXPAND POOL
        Debug.LogWarning("Not enough Obstacles, EXPAND POOL!!!");
        return null;
    }

    protected abstract void OnSpawn(float _speedFactor);

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
