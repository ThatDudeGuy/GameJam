using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public string nextSceneName = "endScreen";
    public bool isGrounded, isJumping, dimensionSwitch = false, can_switch = false, win = false;
    public float jumpForce, duration, fallCheck = 2f, hit_points;
    private const float OVERWORLD_Y = 0.31f;
    private const float UNDERWORLD_Y = -13.8f;
    public const float X_POS = -0.075f;
    public int noMore = 0, scoreValue;
    private Vector3 currentPosition;
    private Vector2 pos;
    private Sequence myJumpTween;
    public Tweener  myFallTween;
    private GameObject[] all_Enemies, spawnUnder, spawnOver, winCheck, switch_check;
    public int wcLength;
    private Animator enemy_Animator;
    public AudioSource swordSwing, jump_grunt;
    //USE myTween.KILL on the jump animation whenever we collide with a floor object. Be it the ground plane or platforms
    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        jumpForce = 5f;
        duration = 1.5f;
        hit_points = 200f;
        scoreValue = 0;
        animator.SetBool("Moving", true);
        animator.SetBool("Falling", false);
        animator.SetBool("Attacking", false);
        transform.localPosition = new Vector3(X_POS, OVERWORLD_Y, 0);
    }
    void Update()
    {   
        winCheck = GameObject.FindGameObjectsWithTag("platform_Overworld");
        wcLength = winCheck.Length;
        switch_check = GameObject.FindGameObjectsWithTag("platform_Underworld");
        if(switch_check.Length == 0 && noMore == 1){
            can_switch = false;
        }
        if(winCheck.Length == 0 && noMore > 1){
            win = true;
        }
        if(can_switch){
            print("jumping = "+animator.GetBool("Jumping")+ " tween = "+myFallTween == null+" falling = "+animator.GetBool("Falling"));
        }
        if(win){
            print("Switching screens");
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadScene("winScreen");
        }
        if(hit_points <= 0){
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadScene("loseScreen");
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping && !animator.GetBool("Rolling")){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 1f;
            //print(fallCheck);
            if(!dimensionSwitch){
                jump(pos.x, OVERWORLD_Y, jumpForce);
            }
            else{
                jump(pos.x, UNDERWORLD_Y, jumpForce);
            }
        }
        if(noMore <= 1){
            if(Input.GetKeyDown(KeyCode.S) && dimensionSwitch == true && can_switch){
                //play error sound here
                print("Already in underworld");
            }
            else if(Input.GetKeyDown(KeyCode.S) && !animator.GetBool("Jumping") && myFallTween == null && !animator.GetBool("Falling") && can_switch){
                noMore++;
                dimensionSwitch = true;
                roll();
                spawnUnder = GameObject.FindGameObjectsWithTag("Spawn_Underworld");
                foreach(var spawner in spawnUnder) {
                    spawner.GetComponent<Enemy_Spawn_Behavior>().Start();
                }
            }
            if(Input.GetKeyDown(KeyCode.W) && dimensionSwitch == false && !can_switch){
                //play error sound here
                print("Already in overworld");
            }
            else if(Input.GetKeyDown(KeyCode.W) && !animator.GetBool("Jumping") && myFallTween == null && !animator.GetBool("Falling") && !can_switch){
                noMore++;
                dimensionSwitch = false;
                roll();
                spawnOver = GameObject.FindGameObjectsWithTag("Spawn_Overworld");
                foreach(var spawner in spawnOver) {
                    spawner.GetComponent<Enemy_Spawn_Behavior>().Start();
                }
            }
        }
        if(Input.GetMouseButtonDown(0) && !animator.GetBool("Hurt")){
            attack();
        }
        currentPosition = transform.localPosition;
        if(currentPosition.y >= fallCheck){
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        if(myFallTween != null){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 1f;
            myFallTween.OnKill(() => {
                if(!dimensionSwitch){
                    transform.localPosition = new Vector3(X_POS, OVERWORLD_Y, 0);
                    myFallTween = null;
                }
                else{
                    transform.localPosition = new Vector3(X_POS, UNDERWORLD_Y, 0);
                    myFallTween = null;
                }
            });
        }
    }
    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.gameObject.name.Contains("Platform")){
            if(transform.localPosition.y >= otherObject.bounds.max.y && animator.GetBool("Falling")){
                myJumpTween.Kill();
                myFallTween.Kill();
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
                isGrounded = true;
                isJumping = false;
            }
        }

        if(otherObject.CompareTag("Underworld_Foreground_LEFT") || otherObject.CompareTag("Underworld_Foreground_RIGHT") 
        || otherObject.CompareTag("Overworld_Foreground_LEFT") || otherObject.CompareTag("Overworld_Foreground_RIGHT")){

            if(!animator.GetBool("Rolling")){
                //print("Canceled JUMP " +otherObject+ " Killed Tween");
                myJumpTween.Kill();
                myFallTween.Kill();
            }
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            isGrounded = true;
            isJumping = false;
        }
        if(otherObject.CompareTag("Enemy")){
            animator.SetBool("Hurt", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Skeleton")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Skeleton", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Mushroom")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Mushroom", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Flying_Eye")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Flying_Eye", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Goblin")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Goblin", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Knight_No_Helmet")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Knight_No_Helmet", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Knight_Full_Armor")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Knight_Full_Armor", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Huntress_Spear")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Huntress_Spear", true);
            hit_points -= 1;
        }
        if(otherObject.CompareTag("Huntress_Bow")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Huntress_Bow", true);
            hit_points -= 1;
        }
    }
    void OnTriggerExit2D(Collider2D otherObject)
    {
        //print(otherObject + "EXIT");
        if(otherObject.CompareTag("Underworld_Foreground_LEFT") || otherObject.CompareTag("Underworld_Foreground_RIGHT") 
        || otherObject.CompareTag("Overworld_Foreground_LEFT") || otherObject.CompareTag("Overworld_Foreground_RIGHT")
        || otherObject.name.Contains("Platform")){

            if(isJumping){
                isGrounded = false;
            }
        } 
        if(otherObject.name.Contains("Platform") && !isJumping){
            animator.SetBool("Falling", true);
            myFallTween = transform.DOMoveY(OVERWORLD_Y, 0.75f, false);
        }
        if(otherObject.CompareTag("Enemy")){
            animator.SetBool("Hurt", false);
        }
        if(otherObject.CompareTag("Skeleton")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Skeleton", false);
        }
        if(otherObject.CompareTag("Mushroom")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Mushroom", false);
        }
        if(otherObject.CompareTag("Flying_Eye")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Flying_Eye", false);
        }
        if(otherObject.CompareTag("Goblin")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Goblin", false);
        }
        if(otherObject.CompareTag("Knight_No_Helmet")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Knight_No_Helmet", false);
        }
        if(otherObject.CompareTag("Knight_Full_Armor")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Knight_Full_Armor", false);
        }
        if(otherObject.CompareTag("Huntress_Spear")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Huntress_Spear", false);
        }
        if(otherObject.CompareTag("Huntress_Bow")){
            iterateEnemies_and_startAttacks(all_Enemies, enemy_Animator, "Huntress_Bow", true);
        }
    }
    private void iterateEnemies_and_startAttacks(GameObject[] enemies, Animator enemies_Animator, string enemyTag, bool setTrue_OR_False){
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var item in enemies)
        {
            enemies_Animator = item.GetComponent<Animator>();
            if(enemies_Animator.GetBool("Death")){
                return;
            }
            if(enemies_Animator.GetBool("Attacking") && !animator.GetBool("Attacking")){
                animator.SetBool("Hurt", setTrue_OR_False);
                hit_points -= 1;
                if(!setTrue_OR_False){
                    enemies_Animator.SetBool("Attacking", false);
                }
                return;
            }
        }
    }
    private Vector2 getPos(){
        Vector3 currentPosition = transform.localPosition;
        Vector2 myPos = (Vector2)currentPosition;
        return myPos;
    }

    void attack(){
        animator.SetBool("Attacking", true);
        swordSwing.Play();
    }
    void endAttack(){
        animator.SetBool("Attacking", false);
    }
    void roll(){
        animator.SetBool("Rolling", true);
        if(myFallTween != null && myFallTween.IsPlaying()){
            return;
        }
        if(dimensionSwitch){
            myFallTween = transform.DOMoveY(UNDERWORLD_Y, 1, false);
        }
        else{
            myFallTween = transform.DOMoveY(OVERWORLD_Y, 1, false);
        }
        //myFallTween.onComplete();
    }
    void endRoll(){
        animator.SetBool("Rolling", false);
    }

    void jump(float x, float y, float force){
        if(!isGrounded){
            animator.SetBool("Jumping", false);
        }
        else if(isGrounded){
            jump_grunt.Play();
            myJumpTween = transform.DOJump(new Vector2(x, y), force, 1, duration, false);
            animator.SetBool("Jumping", true);
            isJumping = true;
        }
    }
}