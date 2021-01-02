using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 音を管理。
/// </summary>
public class SoundDirector : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = default;
    [SerializeField] private List<AudioClip> clips = default;

    /// <summary>
    /// ランダムな音を再生
    /// </summary>
    public void PlaySound()
    {
        int randomID = Random.Range(0, 1);
        float randomPitch = Random.Range(0.8f, 1.2f);

        audioSource.clip = clips[randomID];
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}
