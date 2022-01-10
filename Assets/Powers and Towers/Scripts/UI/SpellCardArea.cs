using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCardArea : MonoBehaviour
{
    public SpellData spellsInDeck;
    public GameObject spellCardPrefab;
    public Transform targetCircle;
    public List<GameObject> spellsInHand;


    public void DrawCard()
    {
        //Debug.Log("Drawing a spell card");
        GameObject newSpellCard = Instantiate(spellCardPrefab, transform, false);
        Vector3 newCardPos = new Vector3(27, -230, 0);
        newSpellCard.transform.localPosition = newCardPos;
        newSpellCard.GetComponent<SpellCard>().spellData = spellsInDeck;
        newSpellCard.GetComponent<SpellCard>().DisplayData();
        newSpellCard.GetComponent<SpellCard>().targetCircle = targetCircle;
        spellsInHand.Add(newSpellCard);
        StartCoroutine(MoveCardUp(newSpellCard, spellsInHand.Count-1));
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

    IEnumerator MoveCardUp(GameObject item, int index)
    {
        Vector3 endPoint = new Vector3(item.transform.localPosition.x, -16 * index);
        while (item.transform.localPosition.y < endPoint.y)
        {
            float step = 256 * Time.deltaTime;
            item.transform.localPosition = Vector3.MoveTowards(item.transform.localPosition, endPoint, step);
            yield return null;
        }
        //item.transform.localPosition = endPoint;
    }

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

    IEnumerator MoveCardDown(GameObject item, int index)
    {
        Vector3 endPoint = new Vector3(item.transform.localPosition.x, -16 * index - 61);
        while (item.transform.localPosition.y > endPoint.y)
        {
            float step = 256 * Time.deltaTime;
            item.transform.localPosition = Vector3.MoveTowards(item.transform.localPosition, endPoint, step);
            yield return null;
        }
        //item.transform.localPosition = endPoint;
    }
}
