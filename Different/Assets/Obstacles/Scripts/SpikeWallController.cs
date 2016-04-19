using UnityEngine;
using System.Collections;

public class SpikeWallController : ObstacleController
{
    private float rot;
    public float speed = 1;

    protected override void Awake()
    {
        base.Awake();

        spikeWallControllers.Add(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        spikeWallControllers.Remove(this);
    }

    public static ObstacleController Spawn(Vector3 _spawnPos, float _spawnDir, float _speedFactor = -1)
    {
        return SpawnBase(spikeWallControllers, _spawnPos, _spawnDir, _speedFactor);
    }

    protected override void OnSpawn(float _speedFactor)
    {
        rot = transform.localEulerAngles.z + 90;
        if (_speedFactor != -1)
        {
            speed *= _speedFactor;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition += speed / 100 * (Vector3)DegreeToVector2(rot);
    }

    private static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
