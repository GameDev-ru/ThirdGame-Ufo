using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ufo : MonoBehaviour
{
    public Rigidbody leftLegRB;
    public Rigidbody rightLegRB;

    public float speed = 10f;
    public float speedRot = 0.8f;

    public SceneLoader sceneLoader;

    public Slider rightSlider;
    public Slider leftSlider;
    public Slider height;

    public Text rightTxt;
    public Text leftTxt;

    private FixedJoint[] fixedJoint;

    private AudioSource[] sounds;


    public GameObject explosion;

    public Animator light;


    void Start()
    {
        light = GetComponent<Animator>();
        explosion.SetActive(false);
        fixedJoint = GetComponentsInChildren<FixedJoint>();

        leftSlider.maxValue = speed * Time.deltaTime;
        rightSlider.maxValue = speed * Time.deltaTime;

        sounds = GetComponents<AudioSource>();

        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    void Update()
    {
        Vector3 minForce = Vector3.up * speed * speedRot * Time.deltaTime;
        Vector3 maxForce = Vector3.up * speed * Time.deltaTime;


        Vector3 leftForce = Vector3.zero;
        Vector3 rightForce = Vector3.zero;



        if (Input.GetKey(KeyCode.A))
        {
            leftForce = minForce;
            rightForce = maxForce;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            leftForce = maxForce;
            rightForce = minForce;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            leftForce = maxForce;
            rightForce = maxForce;

        }
        if (gameObject.transform.position.y < height.maxValue)
        {
            leftLegRB.AddRelativeForce(leftForce);
            rightLegRB.AddRelativeForce(rightForce);

        }



        leftSlider.value = leftForce.y;
        rightSlider.value = rightForce.y;
        height.value = gameObject.transform.position.y;

        if (leftForce.y + rightForce.y > 0 && !sounds[0].isPlaying)
            sounds[0].Play();
        else if (leftForce.y + rightForce.y == 0)
            sounds[0].Pause();

        rightTxt.text = rightForce.y + " Wt";
        leftTxt.text = leftForce.y + " Wt";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            speed = 0;
            explosion.SetActive(true);
            for (int i = 0; i < fixedJoint.Length; i++)
            {
                if (fixedJoint[i] != null)
                    fixedJoint[i].breakForce = 1;
            }
            light.SetBool("Death", true);

        }
        if (collision.gameObject.tag == "Friend")
        {
            //  Debug.Log("Friend");
        }
        if (collision.gameObject.tag == "Finish")
        {

            sceneLoader.NextScene();
        }

    }
}
