using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public GameObject score;
    public GameObject startGameCanvas;
    public GameObject gameCanvas;
    public GameObject endGameCanvas;
    public ParticleSystem explosion;
    public ParticleSystem coinExplosion;

    private float speed = 0;
    private float turningSpeed = 140;
    private float speedIncrement = 0.65f;
    private float turningSpeedIncrement = 4;
    private Text scoreTxt;
    private int nCoins;
    private int direction;
    private GameObject plane;

    void Start() {
        nCoins = 0;

        scoreTxt = score.GetComponent<Text>();
        SetScore(nCoins);

        plane = transform.Find("Plane").gameObject;
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        direction = 0;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            direction -= 1;
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            direction += 1;

        transform.Rotate(Vector3.up, direction * turningSpeed * Time.deltaTime);

        if(plane.transform.localRotation.eulerAngles.z > 330 || plane.transform.localRotation.eulerAngles.z < 30)
            plane.transform.Rotate(Vector3.forward, direction * -100 * Time.deltaTime);
        if(direction == 0) {
            if(plane.transform.localRotation.eulerAngles.z > 0.5f && plane.transform.localRotation.eulerAngles.z < 180)
                plane.transform.Rotate(Vector3.forward, -100 * Time.deltaTime);
            else if(plane.transform.localRotation.eulerAngles.z > 180 && plane.transform.localRotation.eulerAngles.z < 359.5f)
                plane.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Coin":
                coinExplosion.transform.position = other.transform.position;
                coinExplosion.Play();

                Destroy(other.gameObject);

                nCoins++;
                SetScore(nCoins);

                speed += speedIncrement;
                turningSpeed += turningSpeedIncrement;

                if(nCoins == 35) {
                    speed = 0;
                    endGameCanvas.transform.Find("GameResult").GetComponent<Text>().text = "You Win!";

                    gameCanvas.SetActive(false);
                    endGameCanvas.SetActive(true);
                }

                break;
            case "Obstacle":
                explosion.transform.position = transform.position;
                explosion.Play();

                gameCanvas.SetActive(false);
                endGameCanvas.SetActive(true);
                
                Destroy(this.gameObject);

                break;
        }
    }

    private void SetScore(int n) {
        scoreTxt.text = "Score: " + n.ToString();
    }

    public void OnStartButtonClick() {
        speed = 5;
        startGameCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }
}
