using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertController : MonoBehaviour
{
    public enum ObstacleType { SpikeWall, Spear, Sword };

    public ObstacleType obstacleType = ObstacleType.Spear;
    public float direction = 0;
    public float maxTime = 1;
    private float time;
    private Image timer;

    private void Start()
    {
        time = maxTime;

        Transform canvas = transform.GetChild(0);
        Vector2 pos = canvas.position;
        canvas.position = new Vector2(Mathf.Clamp(pos.x, -8.8f, 8.8f), Mathf.Clamp(pos.y, -4.6f, 4.6f));

        timer = transform.GetChild(0).FindChild("AlertTimer").GetComponent<Image>();
    }

    private void Update()
    {
        timer.fillAmount = time / maxTime;

        if (time <= 0)
        {
            Spawn();
        }

        time -= Time.deltaTime;
    }

    private void Spawn()
    {
        switch (obstacleType)
        {
            case ObstacleType.SpikeWall:
                SpikeWallController.Spawn(transform.position, direction);
                break;
            case ObstacleType.Spear:
                SpearController.Spawn(transform.position, direction);
                break;
            case ObstacleType.Sword:
                SwordController.Spawn(transform.position, direction);
                break;
        }

        Destroy(this.gameObject);
    }
}
