using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;
using UnityEngine.UI;

public class AnanasMovement : EnemyMovementBasic
{

    private new void Start()
    {
        base.Start();
        nameUI.text = nameGen.GenerateName("Ananas");
    }

    private new void Update()
    {
        base.Update();
    }

}
