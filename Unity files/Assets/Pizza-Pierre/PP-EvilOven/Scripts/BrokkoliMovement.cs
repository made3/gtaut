using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;
using UnityEngine.UI;

public class BrokkoliMovement : EnemyMovementBasic {
    
    // Use this for initialization
    private new void Start ()
    {
        base.Start();
        nameUI.text = nameGen.GenerateName("Brokkoli");
    }
    private new void Update()
    {
        base.Update();
    }

}
