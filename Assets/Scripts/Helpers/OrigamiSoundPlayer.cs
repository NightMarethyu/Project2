using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrigamiSoundPlayer : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] paperSounds;
    private AudioClip paperClip;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        PlaySound();
    }

    private void PlaySound()
    {
        int index = Random.Range(0, paperSounds.Length);
        paperClip = paperSounds[index];
        source.clip = paperClip;
        source.Play();
        StartCoroutine(WaitBeforeDelete());
    }

    private IEnumerator WaitBeforeDelete()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
