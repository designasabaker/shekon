using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    List<GameObject> darkModeGameObjects;
    List<GameObject> lightModeGameObjects;

    GameObject lookAtTarget;

    private void Awake()
    {
        darkModeGameObjects = GameObject.FindGameObjectsWithTag("DarkMode").ToList<GameObject>();
        lightModeGameObjects = GameObject.FindGameObjectsWithTag("LightMode").ToList<GameObject>();
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
        */
        if(toMode == Mode.ToDark) SetActiveGameObjects(darkModeGameObjects, false); // the LightModeManger will not hide all lightmode objects

        lookAtTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (lookAtTarget == null) return;
        transform.LookAt(lookAtTarget.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        if(toMode == Mode.ToDark)
        {
            SetActiveGameObjects(darkModeGameObjects, true);
            SetActiveGameObjects(lightModeGameObjects, false);
            // gameObject.SetActive(false);
        }
        else
        {
            SetActiveGameObjects(darkModeGameObjects, false);
            SetActiveGameObjects(lightModeGameObjects, true);
            // gameObject.SetActive(false);
        }

    }

    void SetActiveGameObjects(List<GameObject> objList, bool setFlag)
    {
        foreach(GameObject obj in objList)
        {
            obj.SetActive(setFlag);
        }
    }

    public enum Mode
    {
        ToDark,
        ToLight,
    }
}
