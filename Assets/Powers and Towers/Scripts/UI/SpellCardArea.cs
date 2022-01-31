using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellCardArea : MonoBehaviour
{
    public List<SpellData> spellsInDeck;
    public GameObject spellCardPrefab;
    public Transform targetCircle;
    public int maximumHandSize = 7;
    public List<GameObject> spellsInHand;
    private SpellCard currentlyGrabbedCard;
    private bool holdingCard = false;
    public TextMeshProUGUI DrawCost;
    [HideInInspector] public int discount = 0;

    public bool belowMaxHandSize => spellsInHand.Count < maximumHandSize;


    
    private void OnEnable()
    {
        GetSpellsInDeck();
    }

    private void Update()
    {
        DrawCost.text = GetCostToDraw().ToString();
    }

    private void GetSpellsInDeck()
    {
        for (int i = 0; i < GameManager.instance.SpellCards.Length; i++)
        {
            spellsInDeck.Add((SpellData)GameManager.GetCardData(GameManager.instance.SpellCards[i]));
        }
    }

    public int GetCostToDraw()
    {
        int spellCost = 5 + 1 * spellsInHand.Count - discount;
        if (spellCost > 10 - discount)
        {
            spellCost = 10 - discount;
        }
        return spellCost;
    }

    public void DrawCard()
    {
        if (belowMaxHandSize)
        {
            if (GetCostToDraw() <= GameManager.instance.Mana)
            {
                GameManager.instance.Mana -= GetCostToDraw();
                GameObject newSpellCard = Instantiate(spellCardPrefab, transform, false);
                Vector3 newCardPos = new Vector3(27, -230, 0);
                newSpellCard.transform.localPosition = newCardPos;
                int i = Random.Range(0, spellsInDeck.Count);
                newSpellCard.GetComponent<SpellCard>().SpellData = spellsInDeck[i];
                newSpellCard.GetComponent<SpellCard>().targetCircle = targetCircle;
                spellsInHand.Add(newSpellCard);
                StartCoroutine(MoveCardUp(newSpellCard, spellsInHand.Count - 1));
            }
            else
            {
                Debug.Log("Not enough mana to draw"); 
            }
        }
        else
        {
            Debug.Log("Hand is full");
        }
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
