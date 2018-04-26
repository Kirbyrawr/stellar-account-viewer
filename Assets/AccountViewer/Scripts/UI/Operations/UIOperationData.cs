using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using stellar_dotnetcore_sdk.responses.operations;
using AccountViewer.UI.Operations;
using UnityEngine.UI;
using stellar_dotnetcore_sdk.responses;

public abstract class UIOperationData : MonoBehaviour 
{
	public CanvasGroup canvasGroup;

	[HideInInspector]
	public bool showing = false;
	
	private Vector2 showSize;
	private Vector2 hideSize;
	protected UIOperation uiOperation;

	protected TransactionResponse transactionResponse;
	protected OperationResponse operationResponse;

	public abstract float Height 
	{
		get;
	}

	public virtual void Setup(UIOperation uiOperation) 
	{
		this.uiOperation = uiOperation;
		BasicSetup();
		SetupOverview();
		SetupDetails();
	}

	public virtual void BasicSetup() 
	{
		//Set Data
		operationResponse = uiOperation.GetOperationResponse();
		transactionResponse = uiOperation.GetTransactionResponse();
	}

	public virtual void SetupOverview() 
	{
		//Set Date
		uiOperation.dateLabel.text ="";//= System.DateTime.Parse(transactionResponse.CreatedAt).ToShortDateString();
	}

	public abstract void SetupDetails();
}
