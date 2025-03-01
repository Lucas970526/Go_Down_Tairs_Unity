using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int HP;
    [SerializeField] GameObject HpBar;
    //[SerializeField] Text scoreText;
    [SerializeField] TMP_Text scoreText;
    private GameObject currentFloor;
    int score;
    float scoreTime;
    [SerializeField] GameObject smile;
   [SerializeField] GameObject cry;
   [SerializeField] AudioSource audioSource;

   [SerializeField] GameObject replaybutton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP=10;
        score=0;
        scoreTime=0;
        //audioSource = GetComponent<AudioSource>();

        
        if (audioSource != null)
        {
            
            audioSource.loop = true; // ✅ Enable looping if needed
            audioSource.Play();
        }
    }

    // Update is called once per frame

   
   void Update()
    {
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.color = new Color(28f / 255f, 228f / 255f, 255f / 255f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.color = new Color(42f / 255f, 255f / 255f, 28f / 255f); 
        }
        UpdateScore();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // if (smile != null && cry != null)
        // {
        //     smile.position = transform.position;  // Keep smile at player's position
        //     cry.position = transform.position;    // Keep cry at player's position
        // }
        
        if (other.gameObject.tag == "normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("Normal!");
                currentFloor = other.gameObject;
                ModifyHp(5);
                if (smile != null) smile.gameObject.SetActive(true);
                if (cry != null) cry.gameObject.SetActive(false);
                //other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "nails")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("Nails!");
                currentFloor = other.gameObject;
                ModifyHp(-1);
                if (smile != null) smile.gameObject.SetActive(false);
                if (cry != null) cry.gameObject.SetActive(true);
                //other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "ceilling")
        {
            Debug.Log("Ceiling!");

            // 确保 currentFloor 不是 null
            if (currentFloor != null)
            {
                currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            }
            ModifyHp(-3);
            if (smile != null) smile.gameObject.SetActive(false);
            if (cry != null) cry.gameObject.SetActive(true);
            //other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "dealthline")
        {
            Debug.Log("YOU LOSE!");

            if (smile != null) smile.gameObject.SetActive(false);
            if (cry != null) cry.gameObject.SetActive(true);
            scoreText.text="Final Score:"+score.ToString()+" lose!";
            Die();

            //other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    void ModifyHp(int num)
    {
        HP += num;
        if (HP > 10)
        {
            HP = 10;
        }
       
        else if (HP <= 0)
        {
            HP = 0;
            scoreText.text="Final Score:"+score.ToString()+" lose!";
            Die();
        }
        UpdateHpBar();
    }
    void UpdateHpBar()
    {
        Debug.Log("hp"+HpBar.transform.childCount);
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            

            if (HP > i)
            {

                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text="Score:"+score.ToString();
        }
    }
    void Die()
    {
        audioSource.Stop(); 
            
        Time.timeScale=0f;
        replaybutton.SetActive(true);
    }
    public void replay()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("SampleScene");
    }

                                                                                                                                                      



}
