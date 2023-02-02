using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class BeaconManager : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    GameObject lookAtTarget;

    [Header("Caption Settings")]
    public TextMeshProUGUI captionText;
    public float waitingTime = 0.01f;
    public string captionContentIndignous;
    public string captionContentEnglish;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        captionText.text = "";
        lookAtTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (lookAtTarget == null)return;
        transform.LookAt(lookAtTarget.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // The player ENTER the collider of this audio
            Debug.Log("Enter");
            audioSource.Play();
            animator.SetTrigger("Enlarge");
            if(captionText != null) StartCoroutine(TypeCaption()); // typing the caption
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // The player LEAVE the collider of this audio
            audioSource.Stop();
            animator.SetTrigger("Shrink");
            StopAllCoroutines(); // stop typing
        }
    }

    IEnumerator TypeCaption()
    {
        captionText.text = "";
        foreach(char c in captionContentIndignous.ToCharArray())
        {
            captionText.text += c;
            yield return new WaitForSeconds(waitingTime);
        }
        captionText.text += "\n";
        foreach (char c in captionContentEnglish.ToCharArray())
        {
            captionText.text += c;
            yield return new WaitForSeconds(waitingTime);
        }
    }
}
