using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrnTurner : MonoBehaviour {
    Quaternion nextRotation;
    public GameObject gameController;
    int rotationAmount;
    
	void Start () {
		nextRotation = Quaternion.Euler(0, this.transform.rotation.y, 0);
    }
	
	void Update () {
        nextRotation = Quaternion.Euler(0, this.transform.rotation.y + rotationAmount, 0);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, nextRotation, 0.05f);
    }

    private void OnMouseDown() {
        rotationAmount += 90;
        gameController.SendMessage("NextGameState");
    }
}
