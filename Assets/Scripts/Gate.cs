using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gate : MonoBehaviour
{
    public Transform destinationGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Teleport(other.gameObject);
        }
    }

    private void Teleport(GameObject player)
    {
        player.transform.position = destinationGate.position;
       

       
    }

   
}