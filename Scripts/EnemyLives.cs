using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLives : MonoBehaviour
{
    public int maxStage;
    public int initialStage;
    public int stage;
    public int[] lifeAmounts;
    public int lives;

    public Color[] colors;
    public float[] sizes;
    public float[] speed;

    public int[] goldAmounts;

    public GameObject gameUI;

    public GameObject mesh;

    public GameObject achievements;

    //public float emissionIntensity;

    public bool givesDoubleGold;

    public GameObject loseLayerSound;
    public GameObject negateDamageSound;

    public string enemyType;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("Canvas/GameUI");
        achievements = GameObject.Find("Hero").GetComponent<StatsHero>().achievements;

        stage = initialStage;
        lives = lifeAmounts[stage];

        ApplyEnemy();

        if (initialStage >= maxStage)
        {
            achievements.GetComponent<Achievements>().SetNewProgress(8, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage, GameObject damagingObject, string towerType)
    {
        if (damagingObject != null)
        {
            DamageType damageType = damagingObject.GetComponent<DamageType>();

            //Check if the damage type and immunity match
            if (damageType.type == DamageType.Type.Burn && GetComponent<Movement>().isImmuneToBurn)
            {
                PlayNegateDamageSound();
                return;
            }
            if (damageType.type == DamageType.Type.Freeze && GetComponent<Movement>().isImmuneToFreeze)
            {
                PlayNegateDamageSound();
                return;
            }
            if (damageType.type == DamageType.Type.Slow && GetComponent<Movement>().isImmuneToSlow)
            {
                PlayNegateDamageSound();
                return;
            }
            if (damageType.type == DamageType.Type.Poison && GetComponent<Movement>().isImmuneToPoison)
            {
                PlayNegateDamageSound();
                return;
            }
            if (damageType.type == DamageType.Type.Physical && GetComponent<Movement>().isImmuneToPhysical)
            {
                PlayNegateDamageSound();
                return;
            }
        }


        lives -= damage;

        while (lives <= 0)
        {
            if(stage >= 0) gameUI.GetComponent<Gold>().ChangeGold(givesDoubleGold ? goldAmounts[stage] * 2 : goldAmounts[stage]);
            stage--;
            Instantiate(loseLayerSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
            if (stage < 0)
            {
                Die(towerType);
                return;
            }
            int lifeReductionCarryOver = lives;
            lives = lifeAmounts[stage];
            lives += lifeReductionCarryOver;
        }

        ApplyEnemy();

    }
    public void PlayNegateDamageSound()
    {
        Instantiate(negateDamageSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    public void ApplyEnemy()
    {
        mesh.gameObject.GetComponent<MeshRenderer>().material.color = colors[stage];
        //mesh.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", colors[stage] * Mathf.LinearToGammaSpace(emissionIntensity));
        gameObject.transform.localScale = new Vector3(sizes[stage], sizes[stage], sizes[stage]);
        gameObject.GetComponent<Movement>().movementSpeed = speed[stage];
    }
    public void Die(string towerType)
    {
        int currentProgress = Mathf.RoundToInt(achievements.GetComponent<Achievements>().progress[5]);
        achievements.GetComponent<Achievements>().SetNewProgress(5, currentProgress + 1);

        GameObject.Find("Canvas/GameUI/ArtifactPanel").GetComponent<ArtifactPanel>().DropArtifactChance(initialStage, towerType);

        Destroy(gameObject);
    }
}