﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AccountViewer.Controller;

namespace AccountViewer.UI
{
    public abstract class UIModule : MonoBehaviour
    {	
		public Image transparency;

		protected MainController mainController;
		protected UIController uiController;
		protected const float animationSpeed = 0.4f;

		private void Start() 
		{
			InitialSetup();
			Setup();
		}

		private void InitialSetup() 
		{
			uiController = UIController.GetInstance();
			uiController.AddModule(this);
			mainController = uiController.mainController;
		}

		protected virtual void Setup(){}

		#region Transparency
        public void ShowTransparency(float fadeTime = animationSpeed, TweenCallback onComplete = null) 
        {
			transparency.raycastTarget = true;
            transparency.DOFade(0.7f, fadeTime).OnComplete(onComplete);
        }

        public void HideTransparency(float fadeTime = animationSpeed, TweenCallback onComplete = null) 
        {
			transparency.raycastTarget = false;
            transparency.DOFade(0, fadeTime).OnComplete(onComplete);
        }
		#endregion
    }
}