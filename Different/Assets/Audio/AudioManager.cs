using UnityEngine;
using System.Collections;
using DaburuTools.Action;
using DaburuTools;

public class AudioManager : MonoBehaviour
{
	private static AudioManager sInstance = null;
	public static AudioManager Instance { get { return sInstance; } }

	public float BGMVolume = 1.0f;
	public float MainMenuVolume = 0.7f;

	private AudioSource[] mSoundEffects;
	private AudioSource[] mBGM;

	private void Awake()
	{
		if (sInstance == null)
		{
			sInstance = this;
			DontDestroyOnLoad(this.gameObject);

			SetUp();
		}
		else if (sInstance != this)
		{
			Destroy(this.gameObject);
			return;
		}
	}

	private void SetUp()
	{
		// SoundSFX
		mSoundEffects = new AudioSource[transform.GetChild(0).childCount];
		for (int i = 0; i < mSoundEffects.Length; i++)
		{
			mSoundEffects[i] = transform.GetChild(0).GetChild(i).gameObject.GetComponent<AudioSource>();
		}

		// BGM
		mBGM = new AudioSource[transform.GetChild(1).childCount];
		for (int i = 0; i < mBGM.Length; i++)
		{
			mBGM[i] = transform.GetChild(1).GetChild(i).gameObject.GetComponent<AudioSource>();
		}
	}

	public void PlayClickSound()
	{
		mSoundEffects[0].Play();
	}

	public void PlayDeathSound()
	{
		mSoundEffects[1].Play();
	}

	public void PlayReproductionSound()
	{
		mSoundEffects[2].Play();
	}

	public void StartBGM(GameDifficulty _difficulty)
	{
		int track = (int) _difficulty;

		if (mBGM[track].volume == BGMVolume)
			if (mBGM[track].isPlaying)
				return;

		mBGM[track].volume = BGMVolume;
		mBGM[track].Play();

		// Stop playing all other BGMs
		for (int i = 0; i < 3; i++)
		{
			if (i != track)
				mBGM[i].Stop();
		}
	}

	public void StartMenuMusic()
	{
		mBGM[3].volume = 0.0f;

		VolumeFadeAction volumeFade = new VolumeFadeAction(mBGM[3], Graph.Linear, MainMenuVolume, 0.5f);
		ActionHandler.RunAction(volumeFade);

		mBGM[3].Play();
	}

	public void FadeOutBGM()
	{
		for (int i = 0; i < 3; i++)
		{
			VolumeFadeAction volumeFade = new VolumeFadeAction(mBGM[i], Graph.Linear, 0.0f, 0.5f);
			ActionHandler.RunAction(volumeFade);
		}
	}

	public void FadeOutMenuMusic()
	{
		VolumeFadeAction volumeFade = new VolumeFadeAction(mBGM[3], Graph.Linear, 0.0f, 0.5f);
		ActionHandler.RunAction(volumeFade);
	}
}
