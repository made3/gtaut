using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;
using UnityEngine.UI;

public class TomateMovement : EnemyMovementBasic
{
    private float startBaseOffset;
    private float groundBaseOffset;

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        nameUI.text = nameGen.GenerateName("Tomate");
        startBaseOffset = navAgent.baseOffset;
        groundBaseOffset = 1.7f;
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        if (isFollowing)
        {
            if(navAgent.baseOffset > groundBaseOffset)
            {
                navAgent.baseOffset -= 0.05f;
            }
        }
        else
        {
            if (navAgent.baseOffset < startBaseOffset)
            {
                navAgent.baseOffset += 0.05f;
            }
        }

    }

}
