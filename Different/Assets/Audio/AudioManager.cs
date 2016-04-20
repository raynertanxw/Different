using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DaburuTools.Action;
using DaburuTools;

public class AudioManager : MonoBehaviour
{
	private static AudioManager sInstance = null;
	public static AudioManager Instance { get { return sInstance; } }

	public float BGMVolume = 1.0f;
	public float MainMenuVolume = 0.7f;

	private float PlayerBGMVolume = 1.0f;
	private float PlayerSFXVolume = 1.0f;

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

	private void Start()
	{
		if (PlayerPrefs.HasKey(Constants.kBGMVolumeKey))
		{
			GameObject BGMSlider = GameObject.Find("BGM Slider");
			if (BGMSlider != null)
			{
				BGMSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Constants.kBGMVolumeKey);
			}
		}
		if (PlayerPrefs.HasKey(Constants.kSFXVolumeKey))
		{
			GameObject SFXSlider = GameObject.Find("SFX Slider");
			if (SFXSlider != null)
			{
				SFXSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Constants.kSFXVolumeKey);
			}
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

		// Load Player Volume Settings
		if (PlayerPrefs.HasKey(Constants.kBGMVolumeKey))
		{
			PlayerBGMVolume = PlayerPrefs.GetFloat(Constants.kBGMVolumeKey);
		}
		if (PlayerPrefs.HasKey(Constants.kSFXVolumeKey))
		{
			PlayerSFXVolume = PlayerPrefs.GetFloat(Constants.kSFXVolumeKey);
		}
	}

	public void ChangeBGMVolume(float _newVolume)
	{
		PlayerBGMVolume = _newVolume;
		PlayerPrefs.SetFloat(Constants.kBGMVolumeKey, _newVolume);

		// Adjust volume for Menu BGM.
		mBGM[3].volume = MainMenuVolume * PlayerBGMVolume;
	}

	public void ChangeSFXVolume(float _newVolume)
	{
		PlayerSFXVolume = _newVolume;
		PlayerPrefs.SetFloat(Constants.kSFXVolumeKey, _newVolume);

		// Adjust volume for SFXs.
		for (int i = 0; i < mSoundEffects.Length; i++)
		{
			mSoundEffects[i].volume = PlayerSFXVolume;
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

	public void PlayGameOverSound()
	{
		mSoundEffects[3].Play();
	}

	public void StartBGM(GameDifficulty _difficulty)
	{
		int track = (int) _difficulty;

		if (mBGM[track].volume == (BGMVolume * PlayerBGMVolume))
			if (mBGM[track].isPlaying)
				return;

		mBGM[track].volume = BGMVolume * PlayerBGMVolume;
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

		VolumeFadeAction volumeFade = new VolumeFadeAction(mBGM[3], Graph.Linear, MainMenuVolume * PlayerBGMVolume, 0.5f);
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
