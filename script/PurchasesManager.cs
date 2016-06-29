﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
using Prime31;
#endif

public class PurchasesManager : MonoBehaviour {

	protected static PurchasesManager instance = null;
	public static PurchasesManager Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("PurchasesManager");
				if (obj == null) {
					obj = new GameObject("PurchasesManager");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<PurchasesManager> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<PurchasesManager>() as PurchasesManager;
				}
				instance.initialize ();
			}
			return instance;
		}
	}
	public void DummyCall(){
		//Debug.Log ("dummy");
	}
	public enum STATUS {
		NONE		= 0,
		BUYING		,
		SUCCESS		,
		FAILD		,
		MAX			,
	}
	private STATUS m_eStatus;
	public STATUS Status{
		get{ return m_eStatus; }
		set{ m_eStatus = value; }
	}
	public bool m_bPurchased = false;
	private static bool IsInitialized = false;

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	public const string TICKET_010 	=  "ticket010";
	//public const string TICKET_010 	=  "comicticket100";
	public const string TICKET_055 	=  "ticket055";
	public const string TICKET_125 	=  "ticket125";
	public const string TICKET_350 	=  "ticket350";
	public const string TICKET_800 	=  "ticket800";

	public bool IsPurchased(){
		return m_bPurchased;
	}

	public static void buyItem(string productId) {

		instance.m_bPurchased = false;
		instance.m_eStatus = STATUS.BUYING;

		#if UNITY_IPHONE
		//IOSInAppPurchaseManager.Instance.BuyProduct(productId);
		#elif UNITY_ANDROID
		GoogleIAB.purchaseProduct( productId);
		#endif
	}

	public void initialize(){
		if (!IsInitialized) {
			DontDestroyOnLoad(gameObject);

			m_bPurchased = false;

			#if UNITY_IPHONE
			//You do not have to add products by code if you already did it in seetings guid
			//Windows -> IOS Native -> Edit Settings
			//Billing tab.
			/*
			IOSInAppPurchaseManager.Instance.AddProductId(TICKET_010);
			IOSInAppPurchaseManager.Instance.AddProductId(TICKET_055);
			IOSInAppPurchaseManager.Instance.AddProductId(TICKET_125);
			IOSInAppPurchaseManager.Instance.AddProductId(TICKET_350);
			IOSInAppPurchaseManager.Instance.AddProductId(TICKET_800);

			IOSInAppPurchaseManager.OnVerificationComplete += HandleOnVerificationComplete;
			IOSInAppPurchaseManager.OnStoreKitInitComplete += OnStoreKitInitComplete;

			IOSInAppPurchaseManager.OnTransactionComplete += OnTransactionComplete;
			IOSInAppPurchaseManager.OnRestoreComplete += OnRestoreComplete;
			IOSInAppPurchaseManager.instance.LoadStore ();
			*/
			#elif UNITY_ANDROID

			GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
			GoogleIAB.enableLogging (true);
			string key = "your public key from the Android developer portal here";
			// jp.app.bokunozoo
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsqFXrg2t62dru/VFQYyxd2m1kORBbrAGxDxiSAkh3ybaXJtJWNcej/YAxKx7Orrtfq+pU965U2FnU3K54xddts2UGCI9O6TSU0AoKbwFYj+okfF21firsEqZd4aYtVYQ471flWj3ZEG9u2YpIzjGykUQadsxO4Y/OcRbdUn9289Mc0JAbdepmN9yRnvgBJWKZF/c0mBrM4ISfF5TVip2Tp+BXACqblOb+TQZjOB0OeVPxYpdy5k3eJTcQuwiLmYxgpEBL3tIT7grxVROgk8YYncncaZR7Q/wWlsFgFTNMRaF2bPI8apLiA7eIyKv5zbmhbE7YLBXUvkuoHbAqDQrLQIDAQAB";
			// jp.app.bokuno.zoo
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA3xplcFnhYvPYqsxu5D2hP/DwOYHTOUgy59ZUhLaUyEKyK9HZ5hpeDFFe2WqopiR2tM5aD6zRs1WFJqLKjoqoeMi4BwbtAcGOK1I5BDAe9YmDlGN+YEkG6nBPwEm+IZ1C9pLkAi9EoCc28xS/pUlwIPP8/PSMjTpTixO5S0lbKk5tY3VJyt454khCE/XFJMZd6C0j2sBiLwxi7vpZ3i5X0bl75sMr3fvIFdS7WT+m9slwsEZ9qDw/H0Uh01yA5gJn8CkNQ0x04gw+OrtepalTDvE4Lb/nzs6+1QwAi7jbvzPwCD9KDyhfEfLjjj/iOI9nkfjDMXt9d6+n6TCKROfsUwIDAQAB";

			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoNpjSDejTWrxkCnuj5BQ8ozItBVBS2OhgRga4D2zgG42rKy/9C5nb32NDIl+N9xaVh2eMRDVdR9Hzznp0DIE3Xs89le26pzht5dK4/9s01qsVHmuEtecAcXp6ItCieayYSTn9oMgDwd5LWJMQf8+w5vm1qo6Vlo2vh0Lm70DGqisp3pee+6K+Zb+UfPrcvv9tmo3zCpq9EyiPaitw58nSWJYzDuLHzubUj5qeH5OwcAXi/scEkJrD5dJKmkmUgnDTQ2xSP/UAmtN8qAUULej3iOlQCqVIGlSRqL5kA9Qo9fKUX9PU0hcFz6vnuNj9SN3dk/ocAIvvujFKsQjNHvNHQIDAQAB";
			Debug.Log( key );
			//下はテスト用
			//key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArGLKSb92Imt43S40ArCXfTmQ31c+pFQTM0Dza3j/Tn4cqjwccjQ/jej68GgVyGXGC2gT/EtbcVVA+bHugXmyv73lGBgmQlzBL41WYTKolO8Z6pVWTeHBtsT7RcHKukoKiONZ7NiQ9P5t6CCPBB2sXQOp1y3ryVbv01xXlM+hB6HkkKxrT6lIjTbtiVXCHAJvqPexPbqVIfGjBaXH/oHKxEBxYDaa6PTUsU3OP3MTx63ycTEnEMsQlA1W6ZuTFIa5Jd3cVlfQI7uovEzAbIlUfwcnxVOUWASiYe81eQiD1BMl+JeCRhfd5e8D4n0LOA12rHm1F3fC9ZoIEjpNB+BRhwIDAQAB";
			GoogleIAB.init( key );

			#endif

		}
		IsInitialized = true;
	}

	// Use this for initialization
	void Start () {
		initialize ();
		Instance.DummyCall ();
	}

	//public Action<IOSStoreKitResult> OnTransactionComplete = delegate{};
	//public Action<ISN_Result> OnStoreKitInitComplete = delegate{};


	/*
	private static void OnStoreKitInitComplete(ISN_Result result) {
		if(result.IsSucceeded) {
			int avaliableProductsCount = 0;
			foreach(IOSProductTemplate tpl in IOSInAppPurchaseManager.instance.Products) {
				if(tpl.IsAvaliable) {
					avaliableProductsCount++;
				}
			}
			IOSNativePopUpManager.showMessage("StoreKit Init Succeeded", "Available products count: " + avaliableProductsCount);
			Debug.Log("StoreKit Init Succeeded Available products count: " + avaliableProductsCount);
		} else {
			IOSNativePopUpManager.showMessage("StoreKit Init Failed",  "Error code: " + result.Error.Code + "\n" + "Error description:" + result.Error.Description);
		}
	}
	*/



	private static void UnlockProducts(string productIdentifier) {
		Debug.Log (string.Format ("UnlockProducts:{0}", productIdentifier));
		/*
		switch(productIdentifier) {
		case SMALL_PACK:
			//code for adding small game money amount here
			break;
		case NC_PACK:
			//code for unlocking cool item here
			break;
		}
				*/
	}

	#region Android

	#if UNITY_ANDROID

	void billingSupportedEvent()
	{
		Debug.Log( "billingSupportedEvent" );
	}


	void billingNotSupportedEvent( string error )
	{
		Debug.Log( "billingNotSupportedEvent: " + error );
	}


	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
	}


	void queryInventoryFailedEvent( string error )
	{
		Debug.Log( "queryInventoryFailedEvent: " + error );
	}


	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
	}


	void purchaseSucceededEvent( GooglePurchase purchase )
	{
		Debug.Log( "purchaseSucceededEvent: " + purchase );

		GoogleIAB.consumeProduct(purchase.productId);

	}


	void purchaseFailedEvent( string error, int response )
	{
		Debug.Log( "purchaseFailedEvent: " + error + ", response: " + response );

		instance.m_bPurchased = true;
		instance.m_eStatus = STATUS.FAILD;

	}


	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
		instance.m_eStatus = STATUS.SUCCESS;
		m_bPurchased = true;
	}


	void consumePurchaseFailedEvent( string error )
	{
		Debug.Log( "consumePurchaseFailedEvent: " + error );
		instance.m_bPurchased = true;
		instance.m_eStatus = STATUS.FAILD;
	}
	#endif

	#endregion


	// Update is called once per frame
	void Update () {



	
	}
}
