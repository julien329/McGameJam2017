﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpring : ITileEffect {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public float launchAngleDeg = 60;

    [SerializeField]
    private float tileWidth = 2.0f;
    private Rigidbody playerRigidbody;
    private PlayerControlVR selectionOutline;
    private Transform playerTransform;
    private Vector3 targetPos;
    private AudioSource audioSource;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake () {
        GameObject player = GameObject.Find("PlayerVR");
        playerRigidbody = player.GetComponent<Rigidbody>();
        selectionOutline = player.GetComponent<PlayerControlVR>();
        playerTransform = player.transform;
        audioSource = GetComponent<AudioSource>();
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public override void PlayEffect() {
        audioSource.Play();

        targetPos = playerTransform.position + selectionOutline.Direction * tileWidth * 2.0f;
        Vector3 launchImpulse = LaunchVelocity(targetPos, launchAngleDeg);
        playerRigidbody.useGravity = true;
        playerRigidbody.isKinematic = false;
        playerRigidbody.AddForce(launchImpulse, ForceMode.Impulse);

        StartCoroutine(TrackPlayerJump());
    }


    private Vector3 LaunchVelocity(Vector3 targetPos, float angle) {
        Vector3 dir = targetPos - playerTransform.position;
        float height = dir.y;
        dir.y = 0;
        float distance = dir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        dir.y = distance * Mathf.Tan(radAngle);
        distance += height / Mathf.Tan(radAngle);
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radAngle));
        return velocity * dir.normalized;
    }


    IEnumerator TrackPlayerJump() {
        while (playerTransform.position.y >= targetPos.y) {
            yield return null;
        }

        playerRigidbody.useGravity = false;
        playerRigidbody.isKinematic = true;

        playerTransform.position = targetPos;
    }
}
