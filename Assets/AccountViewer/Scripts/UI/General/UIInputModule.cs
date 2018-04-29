using System.Collections;
using System.Collections.Generic;
using AccountViewer.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIInputModule : UIModule
{
    public enum Mode { Add, Edit }

    public abstract string inputName { get; }

	[Header("Generic")]
    public CanvasGroup canvasGroup;
    public Text titleLabel;
    public Text okLabel;

    protected void Show(Mode mode)
    {
		//Canvas
		uiController.inputCanvas.enabled = true;

        //Transparency
        ShowTransparency();
		transparency.GetComponent<Button>().onClick.AddListener(Hide);

        //Canvas Group
        canvasGroup.SetInteractable(true);
        canvasGroup.DOFade(1, animationSpeed);

        switch (mode)
        {
            case Mode.Add:
                titleLabel.text = string.Concat("Add ", inputName);
                okLabel.text = "Add";
                break;

            case Mode.Edit:
                titleLabel.text = string.Concat("Edit ", inputName);
                okLabel.text = "Edit";
                break;
        }
    }

    protected void Hide()
    {
		//Canvas
		uiController.inputCanvas.enabled = false;

        //Transparency
		transparency.GetComponent<Button>().onClick.RemoveAllListeners();
        HideTransparency();

        //Canvas Group
        canvasGroup.SetInteractable(false);
        canvasGroup.DOFade(0, animationSpeed);
    }

	public abstract void OnClickAdd();

	public abstract void OnClickEdit();

    protected abstract bool IsDataValid();

	protected abstract void Reset();
}
