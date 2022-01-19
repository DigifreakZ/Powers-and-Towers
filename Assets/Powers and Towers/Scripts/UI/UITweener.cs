using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITweener : MonoBehaviour
{
    [SerializeField] private Transform targetedEnd;
    private Vector3 startLocation;
    private Vector3 endPosition;
    private float tween = 0f;
    private float animationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
        endPosition = targetedEnd.position;
    }

    IEnumerator GoToEnd()
    {
        while (tween < 1f)
        {
            yield return new WaitForFixedUpdate();
            tween += Time.fixedDeltaTime * animationSpeed;

            transform.position = Vector3.Lerp(startLocation, endPosition, tween);
        }
        yield return null;
    }

    IEnumerator GoToStart()
    {
        while (tween > 0f)
        {
            yield return new WaitForFixedUpdate();
            tween -= Time.fixedDeltaTime * animationSpeed;

            transform.position = Vector3.Lerp(startLocation, endPosition, tween);
        }
        yield return null;
    }


    public void On()
    {
        StopAllCoroutines();
        StartCoroutine("GoToEnd");
    }

    public void Off()
    {
        StopAllCoroutines();
        StartCoroutine("GoToStart");
    }
}
