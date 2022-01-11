using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCardArea : MonoBehaviour
{
    public SpellData spellsInDeck;
    public GameObject spellCardPrefab;
    public Transform targetCircle;
    public List<GameObject> spellsInHand;
    private SpellCard currentlyGrabbedCard;
    private bool holdingCard = false;

    public void DrawCard()
    {
        GameObject newSpellCard = Instantiate(spellCardPrefab, transform, false);
        Vector3 newCardPos = new Vector3(27, -230, 0);
        newSpellCard.transform.localPosition = newCardPos;
        newSpellCard.GetComponent<SpellCard>().spellData = spellsInDeck;
        newSpellCard.GetComponent<SpellCard>().DisplayData();
        newSpellCard.GetComponent<SpellCard>().targetCircle = targetCircle;
        spellsInHand.Add(newSpellCard);
        StartCoroutine(MoveCardUp(newSpellCard, spellsInHand.Count-1));
    }

    public void GrabbedCard(SpellCard grabbedCard)
    {
        currentlyGrabbedCard = grabbedCard;
        holdingCard = true;
    }
    public void MergeTarget(SpellCard mergeTarget)
    {
        if (holdingCard)
        {
            currentlyGrabbedCard.mergeTarget = mergeTarget;
        }
    }
    public void ResetGrab()
    {
        holdingCard = false;
    }

    public void UpdateHand(GameObject spellCard)
    {
        spellsInHand.Remove(spellCard);
        for (int i = 0; i < spellsInHand.Count; i++)
        {
            StartCoroutine(MoveCardUp(spellsInHand[i], i));
        }
    }
    public void NormalHandPosition()
    {
        StopAllCoroutines();
        for (int i = 0; i < spellsInHand.Count; i++)
        {
            StartCoroutine(MoveCardUp(spellsInHand[i], i));
        }
    }

    // Gets the index of a specific spell card, then moves all other spell cards below it, down.
    public void ViewCard(GameObject spellCard)
    {
        StopAllCoroutines();
        int cardToView = spellsInHand.IndexOf(spellCard);
        for (int i = 0 ; i < spellsInHand.Count; i++)
        {
            if (cardToView < i)
            {
                StartCoroutine(MoveCardDown(spellsInHand[i], i));
            }
            else
            {
                StartCoroutine(MoveCardUp(spellsInHand[i], i));
            }
        }
    }

    private IEnumerator MoveCardUp(GameObject item, int index)
    {
        Vector3 endPoint = new Vector3(item.transform.localPosition.x, -16 * index);
        while (item.transform.localPosition.y < endPoint.y)
        {
            float step = 256 * Time.deltaTime;
            item.transform.localPosition = Vector3.MoveTowards(item.transform.localPosition, endPoint, step);
            yield return null;
        }
    }

    private IEnumerator MoveCardDown(GameObject item, int index)
    {
        Vector3 endPoint = new Vector3(item.transform.localPosition.x, -16 * index - 61);
        while (item.transform.localPosition.y > endPoint.y)
        {
            float step = 256 * Time.deltaTime;
            item.transform.localPosition = Vector3.MoveTowards(item.transform.localPosition, endPoint, step);
            yield return null;
        }
    }
}
