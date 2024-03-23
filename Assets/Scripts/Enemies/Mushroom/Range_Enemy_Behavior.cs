using UnityEngine;

public class Range_Enemy_Behavior : MonoBehaviour
{

    public GameObject myProjectile;
    Vector3 spawnPosition;
    private int shotLimit, shotCount = 0;

    private void Start() {
        if(CompareTag("Flying_Eye")){
            print("EYE");
            shotLimit = 5;
        }
        else if(CompareTag("Mushroom")){
            shotLimit = 3;
        }
    }
    void Update()
    {
        spawnPosition = transform.localPosition;
    }

    void spawn(){
        //print(shotLimit+", "+shotCount);
        if(shotCount < shotLimit){
            Instantiate(myProjectile, spawnPosition, Quaternion.identity);
            shotCount++;
        }
    }

//     public void move(){
//         transform.Translate(SKELL_SPEED * Time.deltaTime * Vector3.left);
//     }
}
