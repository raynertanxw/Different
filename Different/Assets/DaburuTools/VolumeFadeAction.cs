using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections.Generic;

namespace DaburuTools
{
	namespace Action
	{
		public class VolumeFadeAction : Action
		{
			AudioSource mAudioSource;
			float mfInitialVolume;
			float mfDesiredVolume;
			float mfActionDuration;
			float mfElaspedDuration;
			Graph mGraph;

			public VolumeFadeAction(AudioSource _audioSource, Graph _graph, float _mfDesiredVolume, float _actionDuration)
			{
				mAudioSource = _audioSource;
				mGraph = _graph;
				SetAction(_mfDesiredVolume, _actionDuration);
			}
			public void SetAction(float _mfDesiredVolume, float _actionDuration)
			{
				mfDesiredVolume = _mfDesiredVolume;
				mfActionDuration = _actionDuration;
			}
			public void SetGraph(Graph _newGraph)
			{
				mGraph = _newGraph;
			}
			private void SetupAction()
			{
				mfInitialVolume = mAudioSource.volume;
				mfElaspedDuration = 0f;
			}
			protected override void OnActionBegin()
			{
				base.OnActionBegin();

				SetupAction(); 
			}



			public override void RunAction()
			{
				base.RunAction();

				if (mAudioSource == null)
				{
					// Debug.LogWarning("DaburuTools.Action: mTransform Deleted prematurely");
					mParent.Remove(this);
					return;
				}

				mfElaspedDuration += Time.deltaTime;

				float t = mGraph.Read(mfElaspedDuration / mfActionDuration);
				mAudioSource.volume = Mathf.Lerp(mfInitialVolume, mfDesiredVolume, t);

				// Remove self after action is finished.
				if (mfElaspedDuration > mfActionDuration)
				{
					mAudioSource.volume = mfDesiredVolume;	// Force it to be the exact position that it wants.
					OnActionEnd();
					mParent.Remove(this);
				}
			}
			public override void MakeResettable(bool _bIsResettable)
			{
				base.MakeResettable(_bIsResettable);
			}
			public override void Reset()
			{
				SetupAction();
			}
		}
	}
}
