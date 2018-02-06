using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Page : MonoBehaviour {

	public CanvasGroup canvasGroup;

	protected Main controller;

	public abstract string ID 
	{ 
		get; 
	} 

	public virtual void Start() 
	{
		controller = Main.GetInstance();
	}

	public void Show() 
	{
		canvasGroup.alpha = 1f;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}

	public void Hide() 
	{
		canvasGroup.alpha = 0f;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}
}
