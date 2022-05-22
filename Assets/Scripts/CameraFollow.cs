using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float asmoothing; //Velocidad de seguimiento de la cámara al player

    Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Calculo la posicion a la que quiero que vaya la camara
        Vector3 targetCamPos = player.position + offset;
        //Vector3.Lerp(muevete desde esta posicion, a esta posicion, con una velocidad tal)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, asmoothing * Time.deltaTime);
        transform.LookAt(player);
    }
}
