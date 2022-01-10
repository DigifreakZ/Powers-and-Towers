using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCardArea : MonoBehaviour
{
    public SpellData spellsInDeck;
    public GameObject spellCardPrefab;
    public Transform targetCircle;
    public List<GameObject> spellsInHand;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        Debug.Log("Drawing a spell card");
        GameObject newSpellCard = Instantiate(spellCardPrefab, transform, false);
        Vector3 newCardPos = new Vector3(27, -16 * spellsInHand.Count, 0);
        newSpellCard.transform.localPosition = newCardPos;
        newSpellCard.GetComponent<SpellCard>().spellData = spellsInDeck;
        newSpellCard.GetComponent<SpellCard>().DisplayData();
        newSpellCard.GetComponent<SpellCard>().targetCircle = targetCircle;
        spellsInHand.Add(newSpellCard);
    }

    public void UpdateHand(GameObject spellCard)
    {
        spellsInHand.Remove(spellCard);
        foreach (var item in spellsInHand)
        {
            StartCoroutine(MoveCard());
        }
    }

    IEnumerator MoveCard()
    {
        yield return null;
    }
}
