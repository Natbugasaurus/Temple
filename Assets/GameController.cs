using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] lionsNorth, lionsEast, lionsSouth, lionsWest;
    public GameObject statue;
    public GameObject player;
    public GameObject mainCamera;

    public AudioSource audioSource;
    public AudioClip stoneSound;
    public AudioClip roarSound;

    Quaternion newRotation;

    public int gameState = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == 0) {

        } else if (gameState == 1) {
            foreach (GameObject lion in lionsNorth) {
                newRotation = Quaternion.Euler(0, 270, 0);
                lion.transform.rotation = Quaternion.Lerp(lion.transform.rotation, newRotation, 0.06f);
                foreach (Transform child in lion.transform) {
                    child.gameObject.SetActive(true);
                }
            }
            foreach (GameObject lion in lionsEast) {
                newRotation = Quaternion.Euler(0, 0, 0);
                lion.transform.rotation = Quaternion.Lerp(lion.transform.rotation, newRotation, 0.06f);
                foreach (Transform child in lion.transform) {
                    child.gameObject.SetActive(true);
                }
            }
            foreach (GameObject lion in lionsSouth) {
                newRotation = Quaternion.Euler(0, 90, 0);
                lion.transform.rotation = Quaternion.Lerp(lion.transform.rotation, newRotation, 0.06f);
                foreach (Transform child in lion.transform) {
                    child.gameObject.SetActive(true);
                }
            }
            foreach (GameObject lion in lionsWest) {
                newRotation = Quaternion.Euler(0, 180, 0);
                lion.transform.rotation = Quaternion.Lerp(lion.transform.rotation, newRotation, 0.06f);
                foreach (Transform child in lion.transform) {
                    child.gameObject.SetActive(true);
                }
            }
        } else if (gameState == 2) {

        } else if (gameState == 3) {
            statue.transform.position = Vector3.Lerp(statue.transform.position, new Vector3(statue.transform.position.x, 12.2f, statue.transform.position.z), 0.05f);
        }
    }

    void NextGameState() {
        if (gameState <= 3) {
            gameState++;
        }

        if (gameState == 1) {
            mainCamera.SendMessage("StartShake", 3.5);
            audioSource.PlayOneShot(stoneSound, 0.05f);
        }

        if (gameState == 2) {
            mainCamera.SendMessage("StartShake", 3.5);
            audioSource.PlayOneShot(roarSound, 0.06f);
        }

        if (gameState == 3) {
            mainCamera.SendMessage("StartShake", 3.5);
            audioSource.PlayOneShot(stoneSound, 0.5f);
        }
    }
}
