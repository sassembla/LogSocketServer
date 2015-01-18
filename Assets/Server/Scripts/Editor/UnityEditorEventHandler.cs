using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class EditorTrigger {
	static EditorTrigger () {
		ServerLogReceiver.Automate();
	}
}