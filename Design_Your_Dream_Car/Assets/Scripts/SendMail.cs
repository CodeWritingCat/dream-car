﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;


public class SendMail : MonoBehaviour {

	public GameObject mail_Button;

	//Track Scene Index to trigger taking the screenshot
	public GameObject start_Button;
	public GameObject restart_Button;
	public GameObject done_Button;
	public GameObject next_Button;
	public GameObject previous_Button;
	private int sceneIndex;

	void Start()
	{
		mail_Button.GetComponent<Button> ().onClick.AddListener (() => { AttachAndMail(); });
		start_Button.GetComponent<Button> ().onClick.AddListener (() => {sceneIndex++;});
		restart_Button.GetComponent<Button> ().onClick.AddListener (() => {sceneIndex = 0;});
		done_Button.GetComponent<Button> ().onClick.AddListener (() => {sceneIndex = 0;});
		next_Button.GetComponent<Button> ().onClick.AddListener (() => {sceneIndex++; CheckToScreenshot();});
		previous_Button.GetComponent<Button>().onClick.AddListener(()=> {sceneIndex--;});
	}

	static void RemoveTakeScreenshot () {
		//removes the file
		File.Delete("Assets/Data/Dream-Car.png");
		Application.CaptureScreenshot("Assets/Data/Dream-Car.png");
		Debug.Log ("Screenshot!");
	}
	
	void AttachAndMail()
	{
		MailMessage mail = new MailMessage();
		
		mail.From = new MailAddress("imalabadmin@imamuseum.org");
		mail.To.Add("mauermcx@gmail.com");
		mail.Subject = "IMA Test Mail";
		mail.Body = "Hello this is a test email from the IMA Dream Car iOS App";

		System.Net.Mail.Attachment attachment;
		attachment = new System.Net.Mail.Attachment("Assets/Data/Dream-Car.png");
		mail.Attachments.Add(attachment);
		
		SmtpClient smtpServer = new SmtpClient("smtp.mandrillapp.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("imalabadmin@imamuseum.org", "fake_password") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");
		
	}

	void CheckToScreenshot()
	{
		if (sceneIndex == 12)
		{
			StartCoroutine(DelayScreenshot());
		}
	}

	IEnumerator DelayScreenshot()
	{
		//pause to wait for the sliding transition
		yield return new WaitForSeconds (.5f);
		//removes the file
		RemoveTakeScreenshot ();
	}

}