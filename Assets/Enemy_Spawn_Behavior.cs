using UnityEngine;

public class Enemy_Spawn_Behavior : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject Knight_Full_Armor, Knight_No_Helmet, Huntress_Spear, Huntress_Bow, Mushroom, Goblin, Flying_Eye, Skeleton;
    public GameObject platformOver, platformUnder;
    private int type;
    private All_Enemy_Behavior spawnCheck;
    private const float PLATFORM_OFFSET = 0.5f;

    void Start()
    {
        spawnPosition = transform.localPosition;
        //on awake in all_enemies, set the spawn_behavior variable spawnCheck to ALL_Enemy_Behvaior. This is the way to do it if we spawn within the update function or in a timer

        //  if it is a melee spawner
        //  generate a random int and instantiate 1 out of 3 enemies at the spawn location
        if(CompareTag("Spawn_Overworld")){
            if(name.Contains("Melee")){
                type = Random.Range(0, 4);
                //type = 0;
                if(type == 0){
                    Instantiate(Knight_Full_Armor, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    Instantiate(Knight_No_Helmet, spawnPosition, Quaternion.identity);
                }
                else{
                    spawnCheck = Huntress_Spear.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = true;
                    Instantiate(Huntress_Spear, spawnPosition, Quaternion.identity);
                }
            }
           else if(name.Contains("Range")){
                type = Random.Range(0, 4);
                //type = 0;
                if(type == 0){
                    Knight_Full_Armor.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Knight_Full_Armor, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.SKELL_SPEED;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    Knight_No_Helmet.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Knight_No_Helmet, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.SKELL_SPEED;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else if(type == 2){
                    Instantiate(Huntress_Bow, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.FLY_EYE_SPEED/2;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else{
                    spawnCheck = Huntress_Spear.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = false;
                    Instantiate(Huntress_Spear, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = 2.5f;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
           }
        }
        else if(CompareTag("Spawn_Underworld")){
            if(name.Contains("Melee")){
                type = Random.Range(0, 4);
                if(type == 0){
                    spawnCheck = Mushroom.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = true;
                    Instantiate(Mushroom, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    Instantiate(Goblin, spawnPosition, Quaternion.identity);
                }
                else{
                    Instantiate(Skeleton, spawnPosition, Quaternion.identity);
                }
            }
            else if(name.Contains("Range")){
                type = Random.Range(0, 4);
                if(type == 0){
                    Skeleton.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Skeleton, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.SKELL_SPEED;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    Goblin.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Goblin, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.GOBLIN_SPEED;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else if(type == 2){
                    spawnCheck = Mushroom.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = false;
                    Instantiate(Mushroom, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = 2.5f;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
                else{
                    Instantiate(Flying_Eye, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformOver.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.FLY_EYE_SPEED;
                    Instantiate(platformOver, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    // void spawnEnemies(string melee_or_range, GameObject enemy_name, float spawnPosition, ){

    // }

    // void Update()
    // {
    //     spawnPosition = transform.localPosition;
    //     Instantiate(enemyType, spawnPosition, Quaternion.identity);
    // }
}
