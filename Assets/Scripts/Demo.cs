using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Foo {
	public int count;
}

[System.Serializable]
public class Bar : Foo {
	public string name;
}

public class Demo : MonoBehaviour {
	public List<Foo> foos = new List<Foo>();
	public Bar bar;

	void Start() {
		foos.Add (new Foo());
		foos.Add (new Bar());
		bar = (Bar)foos [1];
	}
}