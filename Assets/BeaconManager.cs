using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class BeaconManager : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    GameObject lookAtTarget;
    float initialAnimationWaitingTime = 0.01f;
    bool canMove = true;

    NavMeshAgent navMeshAgent;

    [Header("Caption Settings")]
    public TextMeshProUGUI captionText;
    public float waitingTime = 0.01f;
    public string captionContentIndigenous;
    public string captionContentEnglish;
    public GameObject specialImage;

    [Header("Basic Settings")]
    float remainLookAtTime;
    public float lookAtTime;

    [Header("Patrol State")]
    public float patrolRange;
    Vector3 wayPoint;
    Vector3 guardPosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        captionText.text = "";
        lookAtTarget = GameObject.FindGameObjectWithTag("Player");
        initialAnimationWaitingTime = Random.Range(0f, 0.5f);
        StartCoroutine(StartInitialAnimation());
        if (specialImage != null) specialImage.SetActive(false);
        GetNewWayPoint(); // implement wayPoint
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtTarget == null) {
            //Debug.LogError("null lookAtTarget");
            return;
        };
        transform.LookAt(lookAtTarget.transform);
        //Debug.Log("look at player");

        // AI NavMesh
        if (navMeshAgent == null) return;
        if (remainLookAtTime > 0 )
        {
            remainLookAtTime -= Time.deltaTime;
        }
        else
        {
            // only when wait enough time, then get a new point
            GetNewWayPoint();
        }
        navMeshAgent.destination = wayPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // The player ENTER the collider of this audio
            //Debug.Log("Enter");
            audioSource.Play();
            animator.SetTrigger("Enlarge");
            if(captionText != null) StartCoroutine(TypeCaption()); // typing the caption
            if (specialImage != null) specialImage.SetActive(true);

            navMeshAgent.destination = transform.position;
            canMove = false;
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
            if (specialImage != null) specialImage.SetActive(false);

            canMove = true;
        }
    }
    void GetNewWayPoint()
    {
        if (!canMove) return;

        // reset time
        remainLookAtTime = lookAtTime;

        float randomX = UnityEngine.Random.Range(-patrolRange, patrolRange);
        float randomZ = UnityEngine.Random.Range(-patrolRange, patrolRange);

        Vector3 newPoint = new Vector3(guardPosition.x + randomX, transform.position.y, guardPosition.z + randomZ);
        // make sure the wayPoint is inside the nav mesh
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(newPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    IEnumerator TypeCaption()
    {
        captionText.text = "";
        foreach(char c in captionContentIndigenous.ToCharArray())
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

    IEnumerator StartInitialAnimation()
    {
        yield return new WaitForSeconds(initialAnimationWaitingTime);
        animator.SetTrigger("InitalAnimation");
    }
}
