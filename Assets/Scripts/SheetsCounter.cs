using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SheetsCounter : MonoBehaviour
{
    private int sheetsNumber = 0;

    void Start()
    {
        PlayerPickUp.OnSheetPickedUp.AddListener(updateSheetCount);
    }

    private void updateSheetCount()
    {
        sheetsNumber++;
        TextMeshProUGUI sheetsCounter = GetComponent<TextMeshProUGUI>();
        sheetsCounter.text = $"{sheetsNumber} / 5";
    }
}
