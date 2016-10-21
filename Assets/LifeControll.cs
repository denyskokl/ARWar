using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeControll : MonoBehaviour {

    public float playerLife = 100;
    public float playerShotForce = 20;

    private LifeControll enemy;
    private Text textPlayerLife;
    private Slider sliderPlayer;
    private Color startColorPlayer;
    private Animator animator;
    private float fullPlayerLife = 100;

    void Start () {
        textPlayerLife = GetComponentInChildren<Text>();
        sliderPlayer = GetComponentInChildren<Slider>();

        animator = GetComponent<Animator>();

        startColorPlayer = textPlayerLife.color;

        sliderPlayer.maxValue = fullPlayerLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy = other.gameObject.GetComponent<LifeControll>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemy = null;
        }
    }

    public void OnShot()
    {
        if (enemy != null)
        {
            enemy.ReducePlayerLife(playerShotForce);
        }
    }

    public void ReducePlayerLife(float playerShotForce)
    {
        playerLife -= playerShotForce;

        if (playerLife > 0)
        {
            textPlayerLife.text = string.Format("{0:0} ", playerLife);
            textPlayerLife.color = Color.Lerp(startColorPlayer, Color.red, (fullPlayerLife - playerLife) / fullPlayerLife);
        }
        else
        {
            textPlayerLife.text = string.Format("{0:0} ", 0);
            textPlayerLife.color = Color.red;
            sliderPlayer.gameObject.SetActive(false);

            animator.Play("Death");
        }
    }
}
