﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnalyticsTracking : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;


	public GameObject start_Button;
	public GameObject restart_Button;
	public GameObject no_button;



	// Use this for initialization
	void Start () {
		start_Button.GetComponent<Button>().onClick.AddListener(() => { googleAnalytics.StartSession (); });
		restart_Button.GetComponent<Button> ().onClick.AddListener (() => {googleAnalytics.StopSession(); googleAnalytics.DispatchHits();});
		no_button.GetComponent<Button> ().onClick.AddListener (() => {googleAnalytics.StopSession(); googleAnalytics.DispatchHits();});
	}
	

}
