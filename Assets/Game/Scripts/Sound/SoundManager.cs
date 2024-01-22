using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public List<AudioClip> audioList;
    [SerializeField] AudioSource bgmSource;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        SwitchOrPlayBGM("mainmenu_sound");
    }

    public void ButtonClicked()
    {
        PlaySoundsfx("click_sound");
    }

    public void PlaySoundsfx(string sfxTarget)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].name == sfxTarget)
            {
                GameObject sfxObject = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.sfxPooledObject);
                if (!sfxObject)
                {
                    ObjectPooler.sharedInstance.InitSpawnObject(
                        ObjectPooler.sharedInstance.sfxPrefabs,
                        ObjectPooler.sharedInstance.sfxPooledObject,
                        ObjectPooler.sharedInstance.sfxAmountToPool
                        );
                    sfxObject = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.sfxPooledObject);
                }

                sfxObject.gameObject.SetActive(true);

                float audioLength = audioList[i].length;
                Debug.Log($"{audioList[i].name} duration : {audioLength}");

                StartCoroutine(sfxObject.GetComponent<SfxBehavior>().audioTimer(audioLength + 0.1f));

                AudioSource audio = sfxObject.GetComponent<AudioSource>();
                audio.PlayOneShot(audioList[i]);
                return;
            }
        }
    }

    public void SwitchOrPlayBGM(string bgmTarget)
    {
        Debug.Log($"play sound with name : {bgmTarget}");
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].name == bgmTarget)
            {
                if (!bgmSource.clip)
                {

                    bgmSource.clip = audioList[i];
                    bgmSource.Play();
                }
                else if (bgmSource.clip.name != audioList[i].name)
                {
                    bgmSource.clip = audioList[i];
                    bgmSource.Play();
                }
                return;
            }
        }
    }

}
