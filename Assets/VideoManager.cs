using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            videoPlayer.Play();
            // Debug.Log("Play video");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        videoPlayer.Pause();
    }
}
