using UnityEngine;
using System.Collections;

public class SpikeWallController : ObstacleController
{
    private float rot;
    public float speed = 0.1f;

    protected override void OnSpawn()
    {
        rot = transform.localEulerAngles.z + 90;
    }

    private void FixedUpdate()
    {
        transform.localPosition += speed * (Vector3)DegreeToVector2(rot);
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
