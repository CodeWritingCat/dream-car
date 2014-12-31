﻿//Written by Michael Andrew Auer for the Indianapolis Museum of Art Dream Car iPad Application
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

	public GameObject app_Slider;
	public GameObject next_Button;
	public GameObject previous_Button;
	public GameObject restart_Button;
	public GameObject start_Button;

	public Sprite inactive_Button;
	public Sprite active_Button;

	//direction:
	//true: right (next)
	//false: left (previous)
	private bool direction;

	private bool isMoving;

	//Scene Index is to track certain behaviors based on the current screen
	public int scene_index;

	//Parents for keeping navigational buttons hidden on the right screens
	public GameObject hidden_Parent;
	public GameObject navigation_Parent;

	//Here I set event listeners that determine the intended direction and set up the transition animation
	//I also set the restart listener to set the screen's position back to the start screen
	void Start () {
		isMoving = false;
		scene_index = 0;
		next_Button.GetComponent<Button>().onClick.AddListener( () => { direction = true; MakeTransition(app_Slider.transform.position.x); ParentTransitionButtons(); });
		previous_Button.GetComponent<Button>().onClick.AddListener( () => { direction = false; MakeTransition(app_Slider.transform.position.x); ParentTransitionButtons();});
		restart_Button.GetComponent<Button>().onClick.AddListener( () => { app_Slider.transform.localPosition = new Vector3 (0f, 0f); scene_index = 0; ParentTransitionButtons(); next_Button.GetComponent<Button>().interactable = true; next_Button.GetComponent<Image>().sprite = active_Button; });
		start_Button.GetComponent<Button>().onClick.AddListener( () => { direction = true; MakeTransition(app_Slider.transform.position.x); ParentTransitionButtons(); } );
		ParentTransitionButtons();
	}

	//Determines which direction to move and then takes current position of appslider and animates 1024px. Current Pos is offset to maintain the correct movement
	void MakeTransition(float currentPos) {
		if (isMoving == false) {
			if (direction) 
			{
				isMoving = true;
				AnimationCurve transition = AnimationCurve.EaseInOut (0f, currentPos - 512f, .5f, currentPos - 1536f);
				AnimationClip clip = new AnimationClip ();
				clip.SetCurve ("", typeof(Transform), "localPosition.x", transition);
				animation.AddClip (clip, "queued_transition");
				animation.Play ("queued_transition");
				scene_index++;
				DisableProgression();
				StartCoroutine(WaitForAnimation());
			}
			else 
			{
				isMoving = true;
				AnimationCurve transition = AnimationCurve.EaseInOut (0f, currentPos - 512f, .5f, currentPos + 512);
				AnimationClip clip = new AnimationClip ();
				clip.SetCurve ("", typeof(Transform), "localPosition.x", transition);
				animation.AddClip (clip, "queued_transition");
				animation.Play ("queued_transition");
				scene_index--;
				CheckIfReenable();
				StartCoroutine(WaitForAnimation());
			}
		}

	}
	//Checking various screens to hide buttons if necessary. Parents objects to another empty game object below the appSlider in the canvas
	void ParentTransitionButtons ()
	{
		if (scene_index == 0)
		{
			next_Button.transform.parent = hidden_Parent.transform;
			previous_Button.transform.parent = hidden_Parent.transform;
			restart_Button.transform.parent = hidden_Parent.transform;
		}
		if (scene_index == 1)
		{
			next_Button.transform.parent = navigation_Parent.transform;
			previous_Button.transform.parent = navigation_Parent.transform;
			restart_Button.transform.parent = navigation_Parent.transform;
		}
	}
	//Disables next button functionality. Placement of Function is important! Remember Scene Index!
	void DisableProgression ()
	{
		switch (scene_index)
		{
		case 2:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 3:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 4:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 6:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 7:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 8:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 9:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		case 10:
			next_Button.GetComponent<Button>().interactable = false;
			next_Button.GetComponent<Image>().sprite = inactive_Button;
			break;
		default:
			break;
		}
	}
	
	//Timer that re-enables use of navigation after entire slide animation has completed to prevent overlapping
	IEnumerator WaitForAnimation() {
		yield return new WaitForSeconds(.5f);
		isMoving = false;
	}
	//Enables Next button in case of some exceptions where it is needed on reverse
	void CheckIfReenable()
	{
		switch (scene_index) 
		{
		case 1:
			next_Button.GetComponent<Button>().interactable = true;
			next_Button.GetComponent<Image>().sprite = active_Button;
			break;
		case 5:
			next_Button.GetComponent<Button>().interactable = true;
			next_Button.GetComponent<Image>().sprite = active_Button;
			break;
		default:
			break;
		}
		
	}
}
