using System.Collections;
using System.Collections.Generic;
using AccountViewer.Controller;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AccountViewer.UI.Accounts
{
    public class UIAddAccount : UIModule
    {
        public CanvasGroup canvasGroup;
        public InputField nameField;
        public InputField addressField;

        public void Show() 
        {
            //Transparency
            ShowTransparency();

            //Canvas
            canvasGroup.SetInteractable(true);
            canvasGroup.DOFade(1, animationSpeed);
        }

        public void Hide() 
        {
            //Transparency
            HideTransparency();

            //Canvas
            canvasGroup.SetInteractable(false);
            canvasGroup.DOFade(0, animationSpeed);
        }

        public void OnClickAddAccount()
        {
            if (CanAddAccount())
            {
                MainController mainController = uiController.mainController;
                mainController.accounts.AddAccount(nameField.text, addressField.text);

                nameField.text = string.Empty;
                addressField.text = string.Empty;
                Hide();
            }
        }

        private bool CanAddAccount()
        {
            bool canBeAdded = true;

            //Check Name
            if (string.IsNullOrEmpty(nameField.text))
            {
                canBeAdded = false;
            }

            //Check Address
            if (string.IsNullOrEmpty(addressField.text))
            {
                canBeAdded = false;
            }

            else if (addressField.text.Length != 56)
            {
                canBeAdded = false;
            }

            else if (!addressField.text.StartsWith("G"))
            {
                canBeAdded = false;
            }

            return canBeAdded;
        }
    }
}
