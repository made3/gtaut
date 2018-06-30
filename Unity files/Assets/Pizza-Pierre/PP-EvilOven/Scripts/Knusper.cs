using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Knusper : MonoBehaviour {

    public long deleteTime;

    private Stopwatch stopwatch;

	// Use this for initialization
	void Start () {
        stopwatch = new Stopwatch();
        deleteTime *= 1000;
        stopwatch.Start();
    }
	
	// Update is called once per frame
	void Update () {
		if(stopwatch.ElapsedMilliseconds > deleteTime)
        {
            Destroy(this.gameObject);
        }
	}
}
