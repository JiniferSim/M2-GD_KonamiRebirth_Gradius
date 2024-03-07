using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageOfChoice : MonoBehaviour
{
    public GameObject imageOfShip;
    //private void Start()
    //{
    //    imageOfShip.SetActive(false);
    //}
    void Update()
    {
        // Check if any UI element is currently selected
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            imageOfShip.SetActive(true);
        }
        else
        {
            imageOfShip.SetActive(false);
        }
    }
}
