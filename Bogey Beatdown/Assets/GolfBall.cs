using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBall : MonoBehaviour
{
    public bool canPlayGolf = true;

    [HideInInspector]
    public GolfBallUI ui;
    public Transform playerGamePosition;
    public float launchForce = 20f;

    [HideInInspector]
    public float launchPower;
    private Camera ballCamera;

    private bool isMoving = false;
    private bool isCameraReturning = false;

    private float originalCamSize;

    private Camera mainCamera;

    public float cameraReturnSpeed = 1f;
    public float cameraZoomSpeed = 2f;

    private void Awake()
    {     
        ui = GetComponentInChildren<GolfBallUI>();
        ballCamera = GetComponentInChildren<Camera>();
        originalCamSize = ballCamera.orthographicSize;
        
    }

    private void Update()
    {
        if (isMoving)
        {            
            ballCamera.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
            
            if (ballCamera.orthographicSize < originalCamSize)
                ballCamera.orthographicSize = originalCamSize;

            var veloX = GetComponent<Rigidbody2D>().velocity.x;
            GetComponent<Animator>().SetFloat("ballHit", veloX);
            if (veloX == 0f)
            {
                isMoving = false;
                ReturnCamera();
            }
        }
        else if (isCameraReturning)
        {
            var step = cameraReturnSpeed * Time.deltaTime;
            ballCamera.transform.position = Vector3.MoveTowards(ballCamera.transform.position, mainCamera.transform.position, step);
            ballCamera.orthographicSize += cameraZoomSpeed * Time.deltaTime;
            
            if (ballCamera.orthographicSize > mainCamera.orthographicSize)
                ballCamera.orthographicSize = mainCamera.orthographicSize;

            if (ballCamera.transform.position == mainCamera.transform.position)
            {
                isCameraReturning = false;
                mainCamera.enabled = true;
                ballCamera.enabled = false;
                ballCamera.transform.localPosition = new Vector3(0, 0, -10f);
                Global.UnfreezeAllCharacters();
                QuitGame();
            }
        }
    }

    private void ReturnCamera()
    {
        isCameraReturning = true;
    }
    private void StartBallCamera()
    {
        Global.FreezeAllCharacters();
        
        mainCamera = Camera.main;
        ballCamera.orthographicSize = mainCamera.orthographicSize;
        ballCamera.transform.localPosition = new Vector3(0, 0, -10);
        ballCamera.enabled = true;
        mainCamera.enabled = false;
        isMoving = true;
    }

    public void StartBallGame()
    {
        ui.StartGameButton.SetActive(false);
        ui.GolfGameGauge.SetActive(true);

        // move and freeze player
        var player = GameManager.Instance.Player;
        player.FreezeCharacter();
        player.GetComponent<Movement>().currentScaleX = 1;
        player.transform.position = playerGamePosition.position;
        player.transform.localScale = Vector3.one;


        ui.gaugeCursor.StartCursor();
        print("start game");
    }

    public void Fail() {
        print("whoops");
        GameManager.Instance.Player.UnfreezeCharacter();
        var clip = AudioManager.Instance.audioList.ballMiss;
        StartCoroutine(GameManager.Instance.Player.GetComponent<CharacterHit>().Stun(.5f + clip.length));
        AudioManager.PlaySFX(clip, 1f);
        QuitGame();
    }

    public void StartLaunch(float power)
    {
        launchPower = power;
        GameManager.Instance.Player.GetComponent<PlayerInput>().StartSwingAnimation(this);
    }
    public void LaunchBall()
    {
        print("Launch ball with " + launchPower);
        
        ui.GolfGameGauge.SetActive(false);        

        //add physics to ball
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.one * launchForce * launchPower, ForceMode2D.Impulse);

        // follow ball with camera
        if (launchPower == 2)
        {
            StartBallCamera();
            AudioManager.PlaySFX(AudioManager.Instance.audioList.ballHit, 1f);
        }
        else
        {
            AudioManager.PlaySFX(AudioManager.Instance.audioList.ballBlip, 1f);
            QuitGame();
        }
    }

    void QuitGame()
    {
        ui.GolfGameGauge.SetActive(false);
        canPlayGolf = true;
    }
}
