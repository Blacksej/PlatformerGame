using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorBehaviour : MonoBehaviour
{

    bool isDoorLocked = true;
    public GameObject door;

    [SerializeField] public int gemTarget;

    [SerializeField] private Text gemTargetText;

    [SerializeField] ItemCollector collector;

    private void Start()
    {
        gemTargetText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int gemAmount = gemTarget - collector.gemCount;

        if(collision.gameObject.name == "Player")

        if (isDoorLocked && collector.gemCount >= gemTarget)
        {
                Destroy(door);
        }
        else if (isDoorLocked && collector.gemCount < gemTarget)
            {
                if(gemTarget - collector.gemCount == 1)
                {
                    gemTargetText.text = $"YOU NEED {gemAmount} MORE GEM";
                }
                else
                {
                    gemTargetText.text = $"YOU NEED {gemAmount} MORE GEMS";
                }

                StartCoroutine(ShowMessageCoroutine(4));

            }
    }

    public void DoorLockedStatus()
    {
        isDoorLocked = !isDoorLocked;
    }

    private IEnumerator ShowMessageCoroutine(float timeToShow = 4)
    {
        // Show the text
        gemTargetText.enabled = true;

        // Wait for some time
        yield return new WaitForSeconds(timeToShow);

        // Hide the text
        gemTargetText.enabled = false;
    }
}
