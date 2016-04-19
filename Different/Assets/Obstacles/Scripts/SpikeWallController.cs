﻿using UnityEngine;
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

    public static ObstacleController Spawn(Vector3 _spawnPos)
    {
        return SpawnBase(spikeWallControllers, _spawnPos);
    }

    protected override void OnSpawn()
    {
        rot = transform.localEulerAngles.z + 90;
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
