using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour
{
	public static string FormatTime(float _time)
	{
		int ms = (int) ((_time * 1000.0f) % 1000.0f);
		int s = (int) (_time % 60.0f);
		int min = (int) (_time / 60.0f);

		string survivalTime = min.ToString("00") + ":" + s.ToString("00") + ":" + ms.ToString("000");
		return survivalTime;
	}
}
