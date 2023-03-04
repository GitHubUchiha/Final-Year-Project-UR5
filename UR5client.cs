using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.IO;
using System;

public class UR5client : MonoBehaviour {
	
	private bool socketReady;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;
	public Text MText;



	public void ConnectToServer()
	{
		if (socketReady)
			return;
		string host = "127.0.0.1";
		int port = 21;
		string h;
		int p;
		h = GameObject.Find ("IP").GetComponent<InputField> ().text;
		if (h != "")
			host = h;
		int.TryParse (GameObject.Find ("Port").GetComponent<InputField> ().text, out p);
		if (p != 0)
			port = p;
		PlayerPrefs.SetString ("Host", host);
		PlayerPrefs.SetInt("Port", port);
		try{
			socket = new TcpClient(host,port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
			SceneManager.LoadScene("options1", LoadSceneMode.Single);
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


		

	public void playButton()
	{
		Send("play");
	}

	public void stopButton()
	{
		Send("stop");
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
