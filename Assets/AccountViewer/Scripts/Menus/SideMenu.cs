using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideMenu : MonoBehaviour {

	public RectTransform menuRect;
	public List<Page> pages = new List<Page>();

	private Main controller;
	private Page currentPage;
	private bool opened = false;

	public void Show() 
	{
		opened = true;

		DOTween.Kill("SideMenu");
		menuRect.DOAnchorPos(new Vector2(0, menuRect.anchoredPosition.y), 0.5f).SetEase(Ease.OutCubic).SetId("SideMenu");
	}

	public void Hide() 
	{
		opened = false;

		DOTween.Kill("SideMenu");
		menuRect.DOAnchorPos(new Vector2(-1085, menuRect.anchoredPosition.y), 0.5f).SetEase(Ease.InCubic).SetId("SideMenu");
	}

	public void Toggle() 
	{
		if(opened) 
		{
			Hide();
		}
		else 
		{
			Show();
		}
	}

	public void OnTouchPageButton(string id) 
	{
		if(currentPage != null) 
		{
			currentPage.Hide();
		}

		for (int i = 0; i < pages.Count; i++)
		{
			Page page = pages[i];

			if(page.ID == id) 
			{
				currentPage = page;
				currentPage.Show();
				Hide();
				break;
			}
		}
	}
}
