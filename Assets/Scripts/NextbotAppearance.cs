using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextbotAppearance : MonoBehaviour
{
    public readonly UnityEvent OnNextbotDisappear = new UnityEvent();

    [SerializeField] int _disappearingTimeMin = 5;
    [SerializeField] int _disappearingTimeMax = 80;
    private NextbotManager _nextbotManager;

    void Start()
    {
        //Subscribing for required events
        GetComponent<NextbotLogic>().OnNextbotLeftAttackZone.AddListener(disappear);
        GetComponent<NextbotLogic>().OnNextbotEnteredAttackZone.AddListener(cancelDisappearing);

        _nextbotManager = GameObject.Find("NextbotManager").GetComponent<NextbotManager>();
    }

    private void disappear()
    {
        StartCoroutine(disappearing());
    }
    private IEnumerator disappearing()
    {
        yield return new WaitForSeconds(7);
        OnNextbotDisappear.Invoke();
        _nextbotManager.DisappearForTime(_disappearingTimeMin, _disappearingTimeMax);
    }

    private void cancelDisappearing()
    {
        StopAllCoroutines();
    }
}
