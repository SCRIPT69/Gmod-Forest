using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUp : MonoBehaviour
{
    public static UnityEvent OnSheetPickedUp = new UnityEvent();

    [SerializeField] Camera _camera;

    //Providing access for controlling crawling from outside, by PlayerController
    public void ControlPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.CompareTag("sheet"))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) <= 5)
                {
                    GameObject sheet = hit.transform.gameObject;
                    Destroy(sheet);
                    OnSheetPickedUp.Invoke();
                }
            }
        }
    }
}
