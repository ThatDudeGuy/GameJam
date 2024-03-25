using UnityEngine;

public class Enemy_Spawn_Behavior : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject Knight_Full_Armor, Knight_No_Helmet, Huntress_Spear, Huntress_Bow, Mushroom, Goblin, Flying_Eye, Skeleton;
    public GameObject platformOver, platformUnder, demon_beam, player;
    private GameObject[] deleteBeams;
    private int type, stop = 0;
    public float interval = 3f;
    private All_Enemy_Behavior spawnCheck;
    private const float PLATFORM_OFFSET = 0.75f;

    public void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", 0.0f, interval);
        
    }

    void SpawnBeams(){
        if(!player.GetComponent<PlayerMovement>().can_switch && !player.GetComponent<PlayerMovement>().dimensionSwitch){
            player.GetComponent<PlayerMovement>().can_switch = true;
        }
        else if(player.GetComponent<PlayerMovement>().can_switch && !player.GetComponent<PlayerMovement>().dimensionSwitch){
            player.GetComponent<PlayerMovement>().can_switch = true;
        }
        

        if(!player.GetComponent<PlayerMovement>().dimensionSwitch && player.GetComponent<PlayerMovement>().noMore >= 1){
            CancelInvoke("SpawnBeams");
            deleteBeams = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(var beam in deleteBeams){
                Destroy(beam);
            }
        }
        spawnPosition = transform.localPosition;
        spawnPosition.y += 3f;
        if(CompareTag("Spawn_Overworld") && name.Contains("Melee")){
            Instantiate(demon_beam, spawnPosition, Quaternion.identity);
        }
    }

    void Spawn(){
        spawnPosition = transform.localPosition;
        
        if(stop >= 8){
            // if(!player.GetComponent<PlayerMovement>().can_switch && player.GetComponent<PlayerMovement>().dimensionSwitch){
            // player.GetComponent<PlayerMovement>().can_switch = false;
            // }
            // else if(player.GetComponent<PlayerMovement>().can_switch && player.GetComponent<PlayerMovement>().dimensionSwitch){
            //     player.GetComponent<PlayerMovement>().can_switch = false;
            // }
            //some player boolean set to True. Do not allow the player to go down below until the wave has completed spawning
            print("can_switch = " +player.GetComponent<PlayerMovement>().can_switch);
            CancelInvoke("Spawn");
            stop = 0;
            if(player.GetComponent<PlayerMovement>().noMore <= 1){
                InvokeRepeating("SpawnBeams", 12.0f, interval);
            }
        }
        //  if it is a melee spawner
        //  generate a random int and instantiate 1 out of 3 enemies at the spawn location
        if(CompareTag("Spawn_Overworld") && !player.GetComponent<PlayerMovement>().dimensionSwitch){
            if(name.Contains("Melee")){
                type = Random.Range(0, 3);
                //type = 0;
                if(type == 0){
                    spawnCheck = Knight_Full_Armor.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.dont_Move = false;
                    Instantiate(Knight_Full_Armor, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    spawnCheck = Knight_No_Helmet.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.dont_Move = false;
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
                //type = 2;
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
        else if(CompareTag("Spawn_Underworld") && player.GetComponent<PlayerMovement>().dimensionSwitch){
            if(name.Contains("Melee")){
                type = Random.Range(0, 3);
                if(type == 0){
                    spawnCheck = Mushroom.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = true;
                    Instantiate(Mushroom, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    spawnCheck = Goblin.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.dont_Move = false;
                    Instantiate(Goblin, spawnPosition, Quaternion.identity);
                }
                else{
                    spawnCheck = Skeleton.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.dont_Move = false;
                    Instantiate(Skeleton, spawnPosition, Quaternion.identity);
                }
            }
            else if(name.Contains("Range")){
                type = Random.Range(0, 4);
                if(type == 0){
                    Skeleton.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Skeleton, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformUnder.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.SKELL_SPEED;
                    Instantiate(platformUnder, spawnPosition, Quaternion.identity);
                }
                else if(type == 1){
                    Goblin.GetComponent<All_Enemy_Behavior>().dont_Move = true;
                    Instantiate(Goblin, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformUnder.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.GOBLIN_SPEED;
                    Instantiate(platformUnder, spawnPosition, Quaternion.identity);
                }
                else if(type == 2){
                    spawnCheck = Mushroom.GetComponent<All_Enemy_Behavior>();
                    spawnCheck.spawnMelee = false;
                    Instantiate(Mushroom, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformUnder.GetComponent<Movement_Speed>().platformSpeed = 2.5f;
                    Instantiate(platformUnder, spawnPosition, Quaternion.identity);
                }
                else{
                    Instantiate(Flying_Eye, spawnPosition, Quaternion.identity);
                    spawnPosition.y -= PLATFORM_OFFSET;
                    platformUnder.GetComponent<Movement_Speed>().platformSpeed = All_Enemy_Behavior.FLY_EYE_SPEED;
                    Instantiate(platformUnder, spawnPosition, Quaternion.identity);
                }
            }
        }
        stop++;
    }
}
