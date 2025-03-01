using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource menuSong;
    public AudioSource[] gameSong;

    public GameObject gameUI;

    public int currentgameSongNumber;

    //public bool isPlayingMM;
    //public bool[] isPlayingGM;

    // Start is called before the first frame update
    void Start()
    {
        StartMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        /*isPlayingMM = menuSong.isPlaying;

        for (int i = 0; i < isPlayingGM.Length; i++)
        {
            isPlayingGM[i] = gameSong[i].isPlaying;
        }*/
    }
    public void SwitchToMenuMusic()
    {
        StopGameMusic();
        StartMenuMusic();
    }
    public void SwitchToGameMusic()
    {
        StopMenuMusic();
        StartGameMusic();
    }
    public void StartMenuMusic()
    {
        menuSong.Play();
    }
    public void StopMenuMusic()
    {
        menuSong.Stop();
    }
    public void StartGameMusic()
    {
        StartCoroutine("GameMusic");
    }
    public void StopGameMusic()
    {
        //gameSong[currentgameSongNumber].Stop();
        if (currentgameSongNumber == 0)
        {
            gameSong[gameSong.Length - 1].Stop();
        }
        else
        {
            gameSong[currentgameSongNumber - 1].Stop();
        }
        StopAllCoroutines();
    }
    IEnumerator GameMusic()
    {
        gameSong[currentgameSongNumber].Play();
        if (currentgameSongNumber < gameSong.Length - 1)
        {
            currentgameSongNumber++;
        }
        else
        {
            currentgameSongNumber = 0;
        }
        //yield return new WaitForSeconds(gameSong[currentgameSongNumber].clip.length);
        if (currentgameSongNumber == 0)
        {
            //yield return new WaitForSeconds(gameSong[gameSong.Length - 1].clip.length);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(gameSong[gameSong.Length - 1].clip.length));
        }
        else
        {
            //yield return new WaitForSeconds(gameSong[currentgameSongNumber - 1].clip.length);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(gameSong[currentgameSongNumber - 1].clip.length));
        }
        StartCoroutine("GameMusic");
    }

    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }
}
