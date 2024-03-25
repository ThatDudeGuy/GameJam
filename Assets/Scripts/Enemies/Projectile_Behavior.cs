using UnityEngine;

public class Projectile_Behaviour : MonoBehaviour
{
    private const float MOVE_SPEED_X = 3.75f;
    private const float MOVE_SPEED_Y = 1f;
    private PlayerMovement Player;
    private Animator animator;
    private AudioSource arrow_hit;

    public float maxDownwardSpeed = 1.5f; // Adjust this value as needed
    public float minDownwardSpeed = 0.5f; // Adjust this value as needed
    
    private void Start() {
        Player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        arrow_hit = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(CompareTag("Flying_Eye_shot")){
            transform.Translate(MOVE_SPEED_X * 2 * Time.deltaTime * Vector3.left);
            if(Player.transform.localPosition.y >= transform.localPosition.y && gameObject != null && Player.gameObject != null){
                transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.up);
            }
            else if(Player.transform.localPosition.y <= transform.localPosition.y && gameObject != null && Player.gameObject != null){
                transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.down);
            }
        }
        else if(CompareTag("Mushroom_shot")){
            transform.Translate(MOVE_SPEED_X * Time.deltaTime * Vector3.left);
            if(Player.transform.localPosition.y >= transform.localPosition.y && gameObject != null && Player.gameObject != null){
                transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.up);
            }
            else if(Player.transform.localPosition.y <= transform.localPosition.y && gameObject != null && Player.gameObject != null){
                transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.down);
            }
        }
        else if(CompareTag("Huntress_Bow_shot")){
            float difference = transform.localPosition.x - Player.gameObject.transform.localPosition.x;

            // Calculate the factor by which to adjust the downward speed
            float speedFactor = Mathf.Clamp01(Mathf.Abs(difference) / 3.5f); // Adjust maxDifference as needed

            // Interpolate the downward speed between minDownwardSpeed and maxDownwardSpeed based on the speedFactor
            float downwardSpeed = Mathf.Lerp(minDownwardSpeed, maxDownwardSpeed, speedFactor);

            // Move the object downward with the adjusted speed
            transform.Translate(downwardSpeed * Time.deltaTime * Vector3.down);
            transform.Translate(MOVE_SPEED_X * 3f * Time.deltaTime * Vector3.left);

        }
        else if(CompareTag("Huntress_Spear_shot")){
            transform.Translate(MOVE_SPEED_X * 4 * Time.deltaTime * Vector3.left);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(name.Contains("Projectile")){
                arrow_hit.Play();
                Player.hit_points -= 1;
                foreach (AnimatorControllerParameter parameter in animator.parameters)
                {
                    if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name == "Hit")
                    {
                        animator.SetBool("Hit", true);
                    }
                    else{
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void myDestroy(){ 
        if(animator.GetBool("Hit")){
            Destroy(gameObject);
        }        
    }
}