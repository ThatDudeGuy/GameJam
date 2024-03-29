using UnityEngine;

public class Range_Enemy_Behavior : MonoBehaviour
{

    public GameObject myProjectile;
    private Animator animator;
    Vector3 spawnPosition;
    private int shotLimit, shotCount = 0;

    private void Start() {
        animator = GetComponent<Animator>();
        if(CompareTag("Flying_Eye") || CompareTag("Huntress_Spear")){
            shotLimit = 5;
        }
        else if(CompareTag("Mushroom")){
            shotLimit = 3;
        }
        else if(CompareTag("Huntress_Bow")){
            shotLimit = 10;
        }
    }
    void Update()
    {
        spawnPosition = transform.localPosition;
        if(CompareTag("Huntress_Bow")){
            spawnPosition.y += 0.3f;
        }
    }

    void spawn(){
        if(shotCount < shotLimit){
            Instantiate(myProjectile, spawnPosition, Quaternion.identity);
            shotCount++;
        }
        else{
            animator.SetBool("Range", false);
        }
    }
}
