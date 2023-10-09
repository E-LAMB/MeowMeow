using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProgressionShower : MonoBehaviour
{

    public int match_progress;
    public CosmeticsManager cosmetics_manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AwardCosmetic()
    {
        cosmetics_manager.RewardRandomCosmetic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
