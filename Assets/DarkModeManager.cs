using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class DarkModeManager : MonoBehaviour
{
    /*
    [Header("DarkMode Settings")]
    public List<GameObject> darkModeGameObjects;
    public List<GameObject> lightModeGameObjects;
    */
    // Start is called before the first frame update
    [Header("To light mode or to dark mode ?")]
    public Mode toMode;
    [Header("Scene Transition Setting")]
    public Animator blockImgAnim;

    /*
    [Header("Scene Name Settings")]
    public string lightModeSceneName;
    public string darkModeSceneName;
    */
    /*
    List<GameObject> darkModeGameObjects;
    List<GameObject> lightModeGameObjects;
    */

    GameObject lookAtTarget;
    

    private void Awake()
    {
        /*
        darkModeGameObjects = GameObject.FindGameObjectsWithTag("DarkMode").ToList<GameObject>();
        lightModeGameObjects = GameObject.FindGameObjectsWithTag("LightMode").ToList<GameObject>();
        */
    }

    void Start()
    {
        /*
        if(toMode == Mode.ToDark)
        {
            // Hide all darkMode objects
            SetActiveGameObjects(darkModeGameObjects, false);
        }
        else
        {
            SetActiveGameObjects(lightModeGameObjects, true);
        }
        
        if(toMode == Mode.ToDark) SetActiveGameObjects(darkModeGameObjects, false); // the LightModeManger will not hide all lightmode objects
        */
        lookAtTarget = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (lookAtTarget == null) return;
        transform.LookAt(lookAtTarget.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter trigger");
        if (!other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Quit OnTriggerEnter");
            return;
        }
        /*
        if(lightModeSceneName == "") return;
        if(darkModeSceneName == "") return;
        */

        if (toMode == Mode.ToDark)
        {
            /*
            SetActiveGameObjects(darkModeGameObjects, true);
            SetActiveGameObjects(lightModeGameObjects, false);
            // gameObject.SetActive(false);
            */
            StartCoroutine( LoadNextScene("IndignousSceneGX1.2DarkScene"));
        }
        else
        {
            /*
            SetActiveGameObjects(darkModeGameObjects, false);
            SetActiveGameObjects(lightModeGameObjects, true);
            // gameObject.SetActive(false);
            */
            StartCoroutine(LoadNextScene("IndignousSceneGX1.2LightScene"));
        }

    }

    /*
    void SetActiveGameObjects(List<GameObject> objList, bool setFlag)
    {
        foreach(GameObject obj in objList)
        {
            obj.SetActive(setFlag);
        }
    }
    */

    IEnumerator LoadNextScene(string nextSceneName)
    {
        Debug.Log("loading...");
        if (blockImgAnim != null) {
            blockImgAnim.SetTrigger("TriggerSceneEnd");
            Debug.Log("Showing scene end animation");
         };
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }

    public enum Mode
    {
        ToDark,
        ToLight,
    }
}
