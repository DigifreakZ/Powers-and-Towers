using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void NextWave()
    {
        if (MapManager.instance == null) return;

        MapManager.instance.CommandStartNextRound();
    }
}
