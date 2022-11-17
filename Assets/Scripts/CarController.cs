using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CarController : MonoBehaviour
{
    Rigidbody rigi;
    int speed = 15;
    int score = 0;
    public Image pnl;
    public TMP_Text scoreTxt, finishTxt;    
    public AudioClip crash, claps;
    bool isFinishGame = false;
    bool music = true;
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!isFinishGame)
        {
            Moving();
        }
        else
        {
            
        }
        
        FinishSide();
    }
    void Moving()
    {
        rigi.velocity = Vector3.forward * speed;
        float yatay = Input.GetAxis("Horizontal");
        rigi.velocity = new Vector3(yatay * speed, 0, rigi.velocity.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Block"))
        {
            ShowPanel();
            finishTxt.text = "GAME OVER !";
            AudioSource.PlayClipAtPoint(crash, transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            score++;
            scoreTxt.text = "SCORE: " + score;
            Destroy(other.gameObject);
        }
    }
    void FinishSide()
    {
        if (gameObject.transform.position.z >= 225)
        {
            ShowPanel();
            finishTxt.text = "WIN !";
            if (music)
            {
                AudioSource.PlayClipAtPoint(claps, transform.position);
                music = false;
            }
            
        }
    }
    void ShowPanel()
    {
        isFinishGame = true;
        pnl.gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}
