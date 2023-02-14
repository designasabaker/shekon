using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifeTime = 5f;
    float leftTime;
    // Start is called before the first frame update
    void Start()
    {
        leftTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        leftTime -= Time.deltaTime;

        if(leftTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
