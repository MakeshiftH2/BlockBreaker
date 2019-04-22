using UnityEngine;

public class Ball : MonoBehaviour {
  [SerializeField] Paddle paddle1;
  [SerializeField] float xPush = 2f;
  [SerializeField] float yPush = 15f;
  [SerializeField] AudioClip[] ballSounds;
  [SerializeField] float randomFactor = 0.2f;
  
  // state
  Vector2 paddleToBallVector;
  bool hasStarted = false;
  
  // Cached component references
  AudioSource myAudioSource;
  Rigidbody2D myRigidBody2D;
  
  void Start (){
    paddleToBallVector = transform.position - paddle1.transform.position;
    myAudioSource = GetComponent<AudioSource>();
    myRigidBody2D = GetComponent<Rigidbody2D>();
  }
  
  void Update (){
    if (!hasStarted)
    {
      LockedBallToPaddle();
      LaunchOnMouseClick();
    }
    
  }
  
  private void LaunchOnMouseClick(){
    if (Input.GetMouseButtonDown(0))
    {
      hasStarted = true;
      myRigidBody2D.velocity= new Vector2 (xPush, yPush);
    }
  }
  
  private void LockedBallToPaddle(){
    Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
    transform.position = paddlePos + paddleToBallVector;
  }
  
  private void OnCollisionEnter2D(Collision2D collison)
  {
    Vector2  velocityTweak = new Vector2
    (Random.Range(0f, randomFactor),
    Random.Range(0f, randomFactor));
    if (!hasStarted){
      AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
      myAudioSource.PlayOneShot(clip);
      myRigidBody2D.velocity += velocityTweak;
    }
  }
  
}