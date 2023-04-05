using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampTent : MonoBehaviour
{
    [SerializeField] GameObject interactObj;

    private void Start()
    {
        interactObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.textPermitted && other.gameObject.CompareTag("Player"))
        {
            PlayerController.inTentRange = true;
            interactObj.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.textPermitted && other.gameObject.CompareTag("Player"))
        {
            PlayerController.inTentRange = false;
            interactObj.SetActive(false);
        }
    }
}
