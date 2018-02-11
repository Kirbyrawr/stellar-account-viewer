using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AccountViewer.UI
{
    public abstract class UIModule : MonoBehaviour
    {	
		protected UIController uiController;
		public Image transparency;

		protected const float animationSpeed = 0.4f;

		public virtual void Start() 
		{
			Setup();
		}

		private void Setup() 
		{
			uiController = UIController.GetInstance();
			uiController.AddModule(this);
		}

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