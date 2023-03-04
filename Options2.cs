using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.IO;
using System;

public class Options2 : MonoBehaviour {
	
	private bool socketReady;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;
	public Text MText;
	private String host;
	private int port;

	public void Start()
	{
		host = PlayerPrefs.GetString("Host");
		port = PlayerPrefs.GetInt("Port");
		Debug.Log (host);
		ConnectToServer ();
	}


	public void ConnectToServer()
	{
		if (socketReady)
			return;
		try{
			socket = new TcpClient(host,port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
		}
		catch(Exception e) {
			Debug.Log ("Socket error :" + e.Message);
		}

	} 
		

	private void Update()
	{
		Debug.Log (socketReady);
		if (socketReady) {
			if (stream.DataAvailable) {
				string data = reader.ReadLine ();
				if (data != null)
					MText.text = data;
		}
	}
}
		
	public void Send(string data)
	{
		if (!socketReady)
			return;

		writer.WriteLine (data);
		writer.Flush ();
	}

	public void Home()
	{
		
		SceneManager.LoadScene("androidMain", LoadSceneMode.Single);
	}


	public void back()
	{

		SceneManager.LoadScene("options1", LoadSceneMode.Single);
	}
		

	public void OnSend_PopupButton()
	{
		string message2 = GameObject.Find ("Popup Text").GetComponent<InputField> ().text;
		Send (message2);

	}
		
	public void closepopupButton()
	{
		Send("close popup");
	}
	public void isProgramSavedButton()
	{
		Send("isProgramSaved");
	}
	public void programStateButton()
	{
		Send("programState");
	}
	public void polyscopeVersionButton()
	{
		Send("polyscopeVersion");
	}
	public void poweronButton()
	{
		Send("power on");
	}

	public void poweroffButton()
	{
		Send("power off");
	}
	public void brakereleaseButton()
	{
		Send("brake release");
	}
	public void safetymodeButton()
	{
		Send("safetymode");
	}

	private void CloseSocket()
	{
		if (!socketReady)
			return;

		writer.Close ();
		reader.Close ();
		socket.Close ();
		socketReady = false;
	}
				
}
