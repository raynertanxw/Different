using UnityEngine;
using System.Collections;

public class SwordController : ObstacleController
{
    private float rot;
    public float speed = 1;
    public float rotSpeed = 15;

    protected override void Awake()
    {
        base.Awake();

        swordControllers.Add(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        swordControllers.Remove(this);
    }

    public static ObstacleController Spawn(Vector3 _spawnPos, float _spawnDir)
    {
        return SpawnBase(swordControllers, _spawnPos, _spawnDir);
    }

    protected override void OnSpawn()
    {
        rot = transform.localEulerAngles.z + 90;
    }

    private void FixedUpdate()
    {
		if (GameManager.Instance.GameIsPaused)
			return;

        transform.localPosition += speed / 100 * (Vector3)DegreeToVector2(rot);
        transform.Rotate(new Vector3(0, 0, 1), rotSpeed);
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
