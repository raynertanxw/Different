using UnityEngine;
using System.Collections;
using DaburuTools.Action;
using DaburuTools;

public class Level_Prop_HardOrbit : MonoBehaviour
{
	public Transform orbitPivotTransform;
	public float mfCycleDuration = 5.0f;

	private void Awake()
	{
		OrbitAction2D orbit2D = new OrbitAction2D(this.transform, orbitPivotTransform, false, 1,
			Graph.Linear, mfCycleDuration, true);
		ActionHandler.RunAction(new ActionRepeatForever(orbit2D));
	}
}
