using UnityEngine;
using UnityEditor;

using System;

using LSWebSocket;
using LSWebSocket.Server;

class ServerLogReceiver {
	private string address = "ws://127.0.0.1:8826";
	private WebSocketServer server;

	public ServerLogReceiver () {
		server = new WebSocketServer(address);
		server.AddWebSocketService<ServerImpl>("/");
		server.Start();
	}

	public static void Automate () {
		var receiver = new ServerLogReceiver();
	}
}


public class ServerImpl : WebSocketBehavior {

	protected override void OnMessage (MessageEventArgs e) {
		Debug.Log("on:" + e.Data.ToString());
	}
	
	public void SendBack (string message) {
		Send("haaha!");
	}


}