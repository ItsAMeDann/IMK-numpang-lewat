using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sounds[] sounds;
    [Range(0f, 1f)] public float masterVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (PlayerPrefs.HasKey("MasterVolume"))
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);


        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * masterVolume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;

        foreach (Sounds s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = s.volume * masterVolume;
            }
        }

        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    public void Play(string name, Transform parent = null)
    {
        Sounds s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        if (parent == null)
        {
            s.source.Play();
            return;
        }

        AudioSource tempSource = parent.gameObject.AddComponent<AudioSource>();

        tempSource.clip = s.clip;
        tempSource.volume = s.volume * masterVolume;
        tempSource.pitch = s.pitch;
        tempSource.spatialBlend = s.spatialBlend;
        tempSource.loop = s.loop;

        tempSource.Play();

        if (!tempSource.loop)
        {
            Destroy(tempSource, s.clip.length);
        }
    }
    // List of current Audios
    // CityBGM
    // CarEngine
    // Klakson_big
    // Klakson_def
    // Klakson_double
    // Klakson_long
    // Interaction_bel
    // Interaction_cekrek
    // Interaction_negative
    // Interaction_positive
    // Car_kenceng
    // Car_rem
    // Car_tabrak
}
