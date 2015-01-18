using UnityEngine;
using System;

using ClientLSWebSocket;

/**
	・コンストラクタでログの出先を調整する
	・WebSocket接続を行いログを送付する
*/
public class ClientLogEmitter {

	private const string serverPath = "ws://127.0.0.1:8826";
	private static System.Text.StringBuilder buffer = new System.Text.StringBuilder();
	private static WebSocket ws;

	private static string id = "";

	enum LOGGINGSTATE {
		UNOPENED,
		OPENED,
		CLOSED
	};

	private static LOGGINGSTATE state = LOGGINGSTATE.UNOPENED;

	/**
		constructor without new id
	*/
	public ClientLogEmitter () {
		Application.RegisterLogCallback(TransLog);
		Debug.Log("start transLogging.");

		TryConnect();
	}
	

	/**
		constructor with new identity.
	*/
	public ClientLogEmitter (string newId) {
		SetIdentity(newId);
		Application.RegisterLogCallback(TransLog);
		Debug.Log("start transLogging.");

		TryConnect();
	}


	/**
		resettable identity
	*/
	public void SetIdentity (string newId) {
		id = newId;
	}


	public static void TransLog(string logString, string stackTrace, LogType type) {
		var message = type + ":" + logString;
		if (state == LOGGINGSTATE.OPENED) {
			ws.Send(message);
		} else {
			// try to pool log.
			buffer.Append(message + "\n");
		}
	}


	private void TryConnect () {
		ws = new WebSocket(serverPath);

		ws.OnOpen += (sender, e) => {
			var pooledMessage = buffer.ToString();
			buffer = new System.Text.StringBuilder();
			ws.Send(pooledMessage);
        };

        // ws.OnMessage += (sender, e) => {};

        // ws.OnError += (sender, e) => {};

        // ws.OnClose += (sender, e) => {};

        ws.Connect();

		if (ws.ReadyState != ClientLSWebSocket.WebSocketState.Open) {
			// do retry
		}
	}


}