using UnityEngine;

public class Handle_key_Visibility : MonoBehaviour
{
    private SpriteRenderer myRender;
    public PlayerMovement player;
    void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
        myRender.enabled = false;
    }

    
    void Update()
    {
        if(player.can_switch && !player.dimensionSwitch){
            myRender.enabled = true;
        }
        else if(player.can_switch && !player.dimensionSwitch){
            myRender.enabled = false;
        }
        else if(!player.can_switch && player.dimensionSwitch){
            myRender.enabled = true;
        }
        //myRender.enabled = player.can_switch && !player.dimensionSwitch;
    }
}
