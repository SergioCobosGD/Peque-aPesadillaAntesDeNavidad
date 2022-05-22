using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
        transform.Rotate(new Vector3(1,0,0),90);
        transform.Translate(0,0,-0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
