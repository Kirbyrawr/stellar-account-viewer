using System.Collections;
using System.Collections.Generic;
using AccountViewer.Controller;
using AccountViewer.UI.Operations;
using stellar_dotnetcore_sdk.responses.operations;
using UnityEngine;
using UnityEngine.UI;

public class UIPaymentData : UIOperationData
{
    public Text fromAddressLabel;
	private PaymentOperationResponse paymentOperation;

    public override float Height
    {
        get
        {
            return 230f;
        }
    }

    public override void Setup(UIOperation uiOperation)
    {
		paymentOperation = (PaymentOperationResponse)uiOperation.GetOperationResponse();
        base.Setup(uiOperation);
    }

	public override void SetupOverview() 
	{
		base.SetupOverview();
		
		//Check if we are receiving or if we are sending.
        if (paymentOperation.To.AccountId == MainController.GetInstance().accounts.currentAccount.address)
        {
            uiOperation.typeLabel.text = "Received";
        }
        else
        {
            uiOperation.typeLabel.text = "Sent";
        }

        //Check if the asset is native or not.
        if (paymentOperation.AssetType == "native")
        {
            uiOperation.detailsLabel.text = string.Concat(paymentOperation.Amount, " ", "XLM");
        }
        else
        {
            uiOperation.detailsLabel.text = string.Concat(paymentOperation.Amount, " ", paymentOperation.AssetCode);
        }
	}

	public override void SetupDetails()
	{
        fromAddressLabel.text = string.Concat("From: ", UStellarUtils.ShortAddress(paymentOperation.From.AccountId));
	}
}
