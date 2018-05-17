﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iOSPay : MonoBehaviour
{
	public Button m_PayButton;

	void Awake()
	{
		m_PayButton.onClick.AddListener(Pay);
	}

	void OnDestroy()
	{
		m_PayButton.onClick.RemoveListener(Pay);
	}

	void Start()
	{

	}

	void Pay()
	{
		/*
		string orderInfo = "==0XSFASA418URHJ113H9RUIF2NN";
		string result = HookBridge.doAPPay(orderInfo);
		Debug.Log("支付结果: " + result);
        */
		PayInfo payInfo = new PayInfo();
		payInfo.body = "AR会员";
		payInfo.subject = "蜜迩科技";
		payInfo.out_trade_no = System.DateTime.Now.ToString("yyyyMMddhhmmss") + "test";
		payInfo.total_amount = "1";

		StartCoroutine(OnServerSign(payInfo));
	}

	IEnumerator OnServerSign(PayInfo payInfo)
	{
		//+= Delegate;
		WWWForm form = new WWWForm();
		form.AddField("body", payInfo.body);
		form.AddField("subject", payInfo.subject);
		form.AddField("out_trade_no", payInfo.out_trade_no);
		form.AddField("timeout_express", "30m");
		form.AddField("total_amount", payInfo.total_amount);
		string url = "http://122.112.233.193:9090";
		WWW www = new WWW(url, form);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log(www.error);
		}
		Debug.Log(www.text); //加签后的订单

		string orderInfo = www.text;
		string result = HookBridge.doAPPay(orderInfo);
		Debug.Log("支付结果: " + result);
	}
    
	void AliPayLog(string log)
	{
		Debug.Log(log);
		//-= Delegate;
	}
}