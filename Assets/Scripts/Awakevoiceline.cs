using UnityEngine;

public class VoicelinePlayer : MonoBehaviour
{
    public AudioSource voicelineAudioSource;
    public AudioSource backgroundAudioSource;
    public AudioClip[] voicelines;

    public void PlayVoiceline(int clipIndex)
    {
        if (voicelines.Length == 0 || voicelineAudioSource == null || clipIndex < 0 || clipIndex >= voicelines.Length)
            return;

        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Pause();
        }

        voicelineAudioSource.clip = voicelines[clipIndex];
        voicelineAudioSource.Play();

        float clipLength = voicelineAudioSource.clip.length;
        Invoke("RestoreBackgroundAudio", clipLength);
    }

    void RestoreBackgroundAudio()
    {
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Play();
        }
    }
}