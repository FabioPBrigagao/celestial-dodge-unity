using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    SceneManagerEndless managerClass;
    CameraController cameraClass;

    [HideInInspector]
    public bool startPos = false;
    public float timeBtwShoot;
    public Transform firePos;
    public GameObject flyingParticles;
    public GameObject deathParticles;

    private Touch touch;
    private Vector2 touchPosition;
    private Vector2 mousePosition;
    private ParticleSystem.EmissionModule emission;
    private float deltaX;
    private float deltaY;
    private float countTimeBtwShoot;
    private int wave;
    
    private const float INITIAL_POSITION = -10;
    private const float INITIAL_SPEED = 7;
    private const float VERTICAL_BOUNDARIES = 15;
    private const float HORIZONTAL_BOUNDARIES = 8;


    void Start(){
        emission = flyingParticles.GetComponent<ParticleSystem>().emission;
        managerClass = GameObject.FindGameObjectWithTag("Scene Manager").GetComponent<SceneManagerEndless>(); //Create an instance of "Manager_Controller"
        cameraClass = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>(); //Create an instance of "Manager_Controller"
    }


    void FixedUpdate(){
        if (startPos == true){
            PlayerDragMouse();
            PlayerDrag();
        }
    }

    void Update(){
        StartPosition();
        wave = managerClass.waveNumber;
    }

    void StartPosition(){
        if (startPos == false){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, INITIAL_POSITION), INITIAL_SPEED * Time.deltaTime); //Move player towards start position
            if (transform.position.y == INITIAL_POSITION){
                startPos = true;
            }
        }
    }

    //Movement using the mouse
    void PlayerDragMouse() {
        if (Input.GetMouseButton(0)){
            if (wave == 2 || wave == 4 || wave == 0){ //Waves in which player will fire
                Shoot();
            }
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)){
                deltaX = mousePosition.x - transform.position.x;
                deltaY = mousePosition.y - transform.position.y;
            }

            //Players moves
            //Flying particles emission rate increases
            if ((mousePosition.x - deltaX) < HORIZONTAL_BOUNDARIES &&
                                                   (mousePosition.x - deltaX) > -HORIZONTAL_BOUNDARIES &&
                                                   (mousePosition.y - deltaY) > -VERTICAL_BOUNDARIES &&
                                                   (mousePosition.y - deltaY) < VERTICAL_BOUNDARIES){
                transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
                emission.rateOverTime = 40;
            }

            //Flying particles emission rate decreases
            if (Input.GetMouseButtonUp(0)){
                emission.rateOverTime = 20;
            }
        }
    }

    //Movement using the touch
    void PlayerDrag(){
        if (Input.touchCount > 0){
           if(wave == 2 || wave == 4 || wave == 0){ //Waves in which player will fire
                Shoot();
            } 
            touch = Input.GetTouch(0);
            touchPosition = GetLocation(touch);

            if (touch.phase == TouchPhase.Began){
                deltaX = touchPosition.x - transform.position.x;
                deltaY = touchPosition.y - transform.position.y;
            }
            
            //Players moves
            //Flying particles emission rate increases
            if (touch.phase == TouchPhase.Moved && (touchPosition.x - deltaX) < HORIZONTAL_BOUNDARIES &&
                                                   (touchPosition.x - deltaX) > -HORIZONTAL_BOUNDARIES &&
                                                   (touchPosition.y - deltaY) > -VERTICAL_BOUNDARIES &&
                                                   (touchPosition.y - deltaY) < VERTICAL_BOUNDARIES){
                transform.position = new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY);
                emission.rateOverTime = 40;
            }

            //Flying particles emission rate decreases
            if (touch.phase == TouchPhase.Ended){
                emission.rateOverTime = 20;
            }
        }
    }

    void Shoot(){ 
        if(countTimeBtwShoot <= 0){
            BulletController.Fire(firePos, true);
            countTimeBtwShoot = timeBtwShoot;
        }
        countTimeBtwShoot -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Obstacle"){
           Destroy(this.gameObject); 
           Instantiate(deathParticles, transform.position, transform.rotation); 
        }
        
    }

    Vector2 GetLocation(Touch touch){ //Convert touches from pixel location type to Vector2 type
        return Camera.main.ScreenToWorldPoint(touch.position);
    }
}
