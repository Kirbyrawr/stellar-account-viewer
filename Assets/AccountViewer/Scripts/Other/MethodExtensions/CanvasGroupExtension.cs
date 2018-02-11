using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasGroupExtension 
{
	public static void SetInteractable(this CanvasGroup canvasGroup, bool enabled) 
	{
		canvasGroup.interactable = enabled;
		canvasGroup.blocksRaycasts = enabled;
	}
}
