using UnityEngine;
using System.Collections;
using DaburuTools.Action;

public class Level_Prop_MediumRotate : MonoBehaviour
{
	public float mfPeriod = 5.0f;

	void Awake()
	{
		RotateByAction2D rotByAction2D = new RotateByAction2D(this.transform, 360.0f, mfPeriod);
		ActionHandler.RunAction(new ActionRepeatForever(rotByAction2D));
	}
}
