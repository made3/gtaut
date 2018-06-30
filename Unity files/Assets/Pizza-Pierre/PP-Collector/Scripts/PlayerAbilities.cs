using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerAbilities : MonoBehaviour {

    [Header("Basic Objects")]

    public CameraManager cameraManager;
    public Animator animatorMain;
    public Animator animatorArms;

    [Header("Attack Ability")]

    public Slider attackSlider;
    public float attackCooldown;
    public bool isFocusing;

    private Stopwatch attackStopwatch;

   // [HideInInspector]

    public bool canAttack = true;



    // Use this for initialization
    void Start () {
        attackStopwatch = new Stopwatch();
        attackSlider.maxValue = attackCooldown;
        attackSlider.value = attackCooldown;
        attackSlider.minValue = 0;
        attackCooldown *= 1000;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            Attack();
        }

        if (attackStopwatch.IsRunning)
        {
            attackSlider.value = (float)attackStopwatch.ElapsedMilliseconds / 1000;
        }
        if (attackStopwatch.ElapsedMilliseconds >= attackCooldown)
        {
            attackStopwatch.Reset();
            attackStopwatch.Stop();
            canAttack = true;
        }

    }

    public void Attack()
    {
        if (!attackStopwatch.IsRunning)
        {
            if (cameraManager.isFPActive)
            {
                animatorArms.SetTrigger("isAttacking");
                animatorMain.SetTrigger("isAttacking");
            }
            else
            {
                animatorMain.SetTrigger("isAttacking");
            }
            attackStopwatch.Start();
            canAttack = false;
        }

    }
}
