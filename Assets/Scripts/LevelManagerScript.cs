using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// JH - Added the Cinemachine package so that I can declare a variable of
// type CinemachineVirtualCamera
using Cinemachine;

public class LevelManagerScript : MonoBehaviour
{
    public static bool facingRightMainBool = true;
    public CinemachineVirtualCamera virtualCamera;
    public string currentCharacter;
    public static LevelManagerScript instance;
    public CharacterBirdScript CBScript;
    public CharacterGolemScript CGScript;
    public CharacterWolfScript CWScript;
    public int characterMaxHp = 3;
    public int characterCurrentHp;
    public float score = 0;
    public Animator gameoverAnimator;

    public Sound[] audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        instance = this;
        foreach (Sound s in audioManager) // s = sound that we are looking at
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    void Start()
    {
        characterCurrentHp = characterMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // JH - A function that other scripts can call to set the Follow property on
    // the Cinemachine Virtual Camera to a Transfom for it to follow.
    public void setVCamFollow(Transform transformToFollow)
    {
        this.virtualCamera.Follow = transformToFollow;
    }

    public void EnemyHitPlayer( int damage )
    {

            characterCurrentHp -= damage;      
    }

    public void GameOver()
    {
        StartCoroutine("Restart");
    }

    public void NextStage()
    {
        SceneManager.LoadScene("MainScene");
    }

    // AudioManager
    public void PlayAudio (string name)
    {
       Sound s = Array.Find(audioManager, sound => sound.name == name);
        s.source.Play();
    }

    public void StopAudio (string name)
    {
        Sound s = Array.Find(audioManager, sound => sound.name == name);
        s.source.Stop();
    }

    public void AddScore( float addingScore)
    {

        score = score + addingScore;


    }

    IEnumerator Restart()
    {
        gameoverAnimator.SetTrigger("GameOver");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }


    
}
