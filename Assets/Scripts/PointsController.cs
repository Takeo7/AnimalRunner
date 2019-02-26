using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour {

    public int points = 0;

    public ChallengesController cc;

    public int enemysKilled = 0;
    public int meters = 0;

    public void CalculatePoints()
    {
        meters = cc.UIC.metersRun;
        points += (meters / 500)*5;
        points += enemysKilled;
        MainMenuAnimator.instance.UpdatePointText(points);
    }


}
