using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureUsage
{
    Once, //use once
    Toggle // use features more than once
}

public class CoreFeatures : MonoBehaviour
{
    //Properties common way to access code outside of this script
    //You can create a public variable access them in another script, or you can in properties
    //Properties are encapsulated and formated as fields
    //Properties have two ACCESSORS
    //GET Accessor (READ) return other encapsulated variables
    //SET Accessor (WRITE) allocating values to a property
    //Property names are Pascal - PropertyName

    public bool AudioSFXSourceCreated { get; set; }

    //Audio plays when door opens
    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    //Audio plays on close
    [field: SerializeField]
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake ()
    {
        //MakeSFXAudioSource();
    }

    private void MakeSFXAudioSource()
    {
        //if this is equal to null, create it here

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}
