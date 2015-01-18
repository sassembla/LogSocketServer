using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var a = new ClientLogEmitter("I'm A");
	}
	
	int counter = 0;
	
	// Update is called once per frame
	void Update () {
		Debug.Log("hahahah!" + counter);
		counter ++;
	}
}
