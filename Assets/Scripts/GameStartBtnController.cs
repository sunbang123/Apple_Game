using System.Collections;
using UnityEngine;

public class GameStartBtnController : MonoBehaviour
{
    [SerializeField]
    private AudioSource startAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startAudio = GetComponent<AudioSource>();
    }
    public void BtnClick()
    {
        StartCoroutine(BtnCoroutine());
    }
    IEnumerator BtnCoroutine()
    {
        yield return StartCoroutine(FirstCoroutine());
    }

    IEnumerator FirstCoroutine()
    {
        startAudio.Play();
        yield return new WaitForSeconds(0.1f);
    }
}
