using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{

    // Each ID should begin with a #

    // CH = Cosmetic Head (So the type of cosmetic)
    // _TEMP_ = TEMP COLLECTION (This cosmetic belongs to the Temp Collection)
    // DEF / LIME1 = DEFAULT / LIME 1 (The cosmetic within the collection)

    /* Current cosmetics
     
    HEAD ACCESSORIES
    * Default Cosmetic (Temp)        #CH_TEMP_DEF
    * Pretty Pink Bow  (Temp)        #CH_TEMP_PINK1
    * Shallow Shores Bow (Temp)      #CH_TEMP_BLUE1

    BODY COSMETICS
    * Default Cosmetic (Temp)        #CB_TEMP_DEF
    * Agitating Red Shirt (Temp)     #CB_TEMP_RED1
    * Behind The Shirt (Temp)        #CB_TEMP_PURPLE1
    * Biting The Blue (Temp)         #CB_TEMP_BLUE1
    * Lime Shirt (Temp)              #CB_TEMP_LIME1
    * Mellow Yellow Shirt (Temp)     #CB_TEMP_YELLOW1

    LEG COSMETICS
    * Default Cosmetic (Temp)        #CL_TEMP_DEF
    * Calm Red Pants (Temp)          #CL_TEMP_RED1
    * Lime and Lime Pants (Temp)     #CL_TEMP_LIME1
    * Darker Blue Pants (Temp)       #CL_TEMP_BLUE1
    
    */

    public NewDressup my_new_dressup;

    public string current_cosmetics = "#CH_TEMP_DEF#CB_TEMP_DEF#CL_TEMP_DEF";

    public string[] cosmetics_to_earn;

    public bool give_random;

    public void RewardRandomCosmetic()
    {
        string chosen = "nothinghere";
        int chosen_int = 0;
        int limit_break = 0;

        while (chosen == "nothinghere" && limit_break < 500)
        {
            chosen_int = Random.Range(0, cosmetics_to_earn.Length);
            chosen = cosmetics_to_earn[chosen_int];
            limit_break += 1;
        }

        if (limit_break == 500) { Debug.Log("LIMIT BROKEN"); }

        cosmetics_to_earn[chosen_int] = "nothinghere";
        current_cosmetics += "#" + chosen;

        my_new_dressup.ReadCosmetics();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (give_random)
        {
            give_random = false;
            RewardRandomCosmetic();
        }
    }
}
