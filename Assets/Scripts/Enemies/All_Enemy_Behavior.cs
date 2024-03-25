using UnityEngine;

public class All_Enemy_Behavior : MonoBehaviour
{
   public const float SKELL_SPEED = 2.5f;
   private AudioSource knight_death_sound, huntress_death_sound, demon_death_sound;
   public const float MUSH_SPEED = 1.5f;
   public const float FLY_EYE_SPEED = 4.5f;
   public const float GOBLIN_SPEED = 5f;
   private Vector3 meleeDistance, rangeDistance, meleeCheck, rangeCheck;
   public Animator enemy_Animator, playerAnimator;
   private GameObject Player;
   private const float PLAYER_X = PlayerMovement.X_POS;
   public bool spawnMelee, dont_Move;
    void Start(){
        enemy_Animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        playerAnimator = Player.GetComponent<Animator>();
        meleeDistance = new Vector3(2,0,0);
        rangeDistance = new Vector3(13,0,0);
        
    }
    private void Awake() {
        try{
            if(CompareTag("Mushroom") || CompareTag("Huntress_Spear")){
                enemy_Animator = GetComponent<Animator>();
                enemy_Animator.SetBool("Moving", spawnMelee);
            }
        } catch(System.Exception ex){
            print(ex.Message);
        }
    }
    void Update()
    {
        //print(name+" = "+dont_Move);
        if(!dont_Move){
            if(CompareTag("Skeleton") || CompareTag("Goblin") || CompareTag("Knight_No_Helmet") || CompareTag("Knight_Full_Armor")){
                enemy_Animator.SetBool("Moving", true);
            }
        }
        if(enemy_Animator.GetBool("Death") && !dont_Move && enemy_Animator.GetBool("Moving")){
            transform.Translate(5 * Time.deltaTime * Vector3.left);
        }
        else{
            foreach (AnimatorControllerParameter parameter in enemy_Animator.parameters){
                if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name == "Moving"){
                    if(enemy_Animator.GetBool("Moving") || dont_Move){
                    meleeCheck = transform.localPosition - meleeDistance;
                        if(PLAYER_X >= meleeCheck.x && PLAYER_X < transform.localPosition.x){
                            enemy_Animator.SetBool("Attacking", true);
                        }
                    }
                    else{
                        rangeCheck = transform.localPosition - rangeDistance;
                        if(PLAYER_X >= rangeCheck.x && PLAYER_X < transform.localPosition.x){
                            enemy_Animator.SetBool("Range", true);
                        }
                    }
                }
            }

            if(CompareTag("Skeleton") || name.Contains("Knight")) {
                transform.Translate(SKELL_SPEED * Time.deltaTime * Vector3.left);
            }

            if(CompareTag("Mushroom")) {
                if(!enemy_Animator.GetBool("Moving")){
                    transform.Translate(Movement_Speed.MOVE_SPEED/2 * Time.deltaTime * Vector3.left);
                }
                else{
                    transform.Translate(MUSH_SPEED * Time.deltaTime * Vector3.left);
                }
            }
            if(CompareTag("Flying_Eye")) {
                transform.Translate(FLY_EYE_SPEED * Time.deltaTime * Vector3.left);
            }
            if(CompareTag("Goblin")) {
                transform.Translate(GOBLIN_SPEED * Time.deltaTime * Vector3.left);
            }
            if(CompareTag("Huntress_Spear")) {
                if(!enemy_Animator.GetBool("Moving")){
                    transform.Translate(Movement_Speed.MOVE_SPEED/2f * Time.deltaTime * Vector3.left); //IF THEY ARE NOT MOVING, THEY SHOULD BE MOVING THE SAME AS THE PLATFORM SPEED.X
                }
                else{
                    transform.Translate(GOBLIN_SPEED * Time.deltaTime * Vector3.left);
                }
            }
            if(CompareTag("Huntress_Bow")) {
                transform.Translate(FLY_EYE_SPEED/2 * Time.deltaTime * Vector3.left);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(CompareTag("Skeleton") && playerAnimator.GetBool("Attacking")){
                demon_death_sound = GetComponent<AudioSource>();
                demon_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 50;
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Mushroom") && playerAnimator.GetBool("Attacking")){
                demon_death_sound = GetComponent<AudioSource>();
                demon_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 100;
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Goblin") && playerAnimator.GetBool("Attacking")){
                demon_death_sound = GetComponent<AudioSource>();
                demon_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 75;
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Flying_Eye") && playerAnimator.GetBool("Attacking")){
                demon_death_sound = GetComponent<AudioSource>();
                demon_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 125;
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Knight_No_Helmet") && playerAnimator.GetBool("Attacking")){
                knight_death_sound = GetComponent<AudioSource>();
                Player.GetComponent<PlayerMovement>().scoreValue += 75;
                knight_death_sound.Play();
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Knight_Full_Armor") && playerAnimator.GetBool("Attacking")){
                knight_death_sound = GetComponent<AudioSource>();
                Player.GetComponent<PlayerMovement>().scoreValue += 50;
                knight_death_sound.Play();
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Huntress_Spear") && playerAnimator.GetBool("Attacking")){
                huntress_death_sound = GetComponent<AudioSource>();
                huntress_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 100;
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Huntress_Bow") && playerAnimator.GetBool("Attacking")){
                huntress_death_sound = GetComponent<AudioSource>();
                huntress_death_sound.Play();
                Player.GetComponent<PlayerMovement>().scoreValue += 150;
                enemy_Animator.SetBool("Death", true);
            }
        }
    }
}