using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.IO;
using System;

public class Options1 : MonoBehaviour {
	
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

	public void next()
	{
		SceneManager.LoadScene("options2", LoadSceneMode.Single);
	}
		
	public void OnSend_LoadButton()
	{
		string message1 = GameObject.Find ("Program Name").GetComponent<InputField> ().text;
		Send (message1);

	}

	public void playButton()
	{
		Send("play");
	}

	public void stopButton()
	{
		Send("stop");
	}

	public void pauseButton()
	{
		Send("pause");
	}

	public void quitButton()
	{
		Send("quit");
	}

	public void shutdownButton()
	{
		Send("shutdown");
	}

	public void runningButton()
	{
		Send("running");
	}
	public void robotmodeButton()
	{
		Send("robotmode");
	}
	public void getloadedprogramButton()
	{
		Send("get loaded program");
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
