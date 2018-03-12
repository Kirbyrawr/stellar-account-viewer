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
    public CanvasGroup canvasGroup;
    public Text titleLabel;
    public Text okLabel;

    protected void Show(Mode mode)
    {
        //Transparency
        ShowTransparency();

        //Canvas
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
        //Transparency
        HideTransparency();

        //Canvas
        canvasGroup.SetInteractable(false);
        canvasGroup.DOFade(0, animationSpeed);
    }

	protected abstract void OnClickAdd();

	protected abstract void OnClickEdit();

    protected abstract bool IsDataValid();
}
