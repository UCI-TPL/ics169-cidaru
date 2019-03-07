using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack {
    /* Defines how the Boss attacks
     * All Phases: Charges at player
     * Phase 1: Incrementally summons enemy spawners
     * Phase 2: Bullet Hell
     */

    [Header("Base Info")]
    public int dmg = 3;
    public float phase1Percent = (float)0.75; //the percent of health before the boss enters phase1
    public float phase2Percent = (float)0.25; //the percent of health before the boss enters phase2

    /// HP stuff
    private Health hp;
    private int phase1Health;
    private int phase2Health;

    /// Extra Charge stuff
    private float originalChargeDist;
    private ChargeMovement chargeMove;

    // Animation
    private Animator anim;

    [Header("Phase 1")]
    /// Spawning stuff
    public GameObject spawner;
    public float spawnRate; //the rate at which it spawns spawners
    public float spawnDuration; //the amount of time it takes to spawn

    private float spawnerTimer;
    private float spawnDurationTimer;

    [Header("Phase 2")]
    public int numCharges = 1;
    public float bulletHellDuration = 3f;
    public float setFireTimer = 1f;
    public GameObject bullet;
    public GameObject gunHolder;
    public GameObject shield;

    /// Timers
    private float hellTimer;
    private float fireTimer;
    private float pulseTimer;

    /// Misc important
    private Animator gunHolderAnim;
    private LevelsOFHell currentHell;

    [Header("FOR TESTING ONLY")]
    public bool demo = false; //if true, checks below to see which phase and runs it
    public Phase phase;

    public enum Phase
    { One, Two}

    public enum LevelsOFHell
    {
        Basic,
        Wiggly,
        Pulse,
        HalfCirc,
        Arc,
        AimedArc,
        FrontBlast,
        AimedFrontBlast,
        HarderBasic,
        HarderWiggly,
        HarderPulse
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponent<Animator>();

        hp = GetComponent<Health>();
        phase1Health = (int) (phase1Percent * hp.startingHealth);
        phase2Health = (int) (phase2Percent * hp.startingHealth);

        spawnerTimer = spawnRate;
        spawnDurationTimer = 0;

        hellTimer = 0f;
        fireTimer = setFireTimer;
        pulseTimer = 0f;
        currentHell = LevelsOFHell.Basic;
        gunHolderAnim = gunHolder.GetComponent<Animator>();
        shield.SetActive(false);
    }

    private void Awake()
    {
        originalChargeDist = GetComponent<ChargeMovement>().chargeDistance;
        chargeMove = GetComponent<ChargeMovement>();
    }

    public override void Attack()
    {
        if (demo)
            phaseDemo();
        else
            checkPhase();
    }

    private void checkPhase()
    {
        if (hp.currentHealth >= phase2Health && hp.currentHealth <= (phase1Health))
        {
            Phase1();
        }
        else if (hp.currentHealth <= (phase2Health))
            Phase2();
    }

    private void phaseDemo()
    {
        if (phase == Phase.One)
        {
            Phase1();
        }
        else if (phase == Phase.Two)
        {
            Phase2();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") // Hit player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }
    }

    private void Phase1()
    {
        ///Birthing some dudes
        spawnerTimer += Time.deltaTime;

        if (spawnerTimer >= spawnRate)
        {
            spawnDurationTimer += Time.deltaTime;
            chargeMove.cancelCharge();
            chargeMove.enabled = false;
            hp.setInvincible(true);
            shield.SetActive(true);

            if (spawnDurationTimer >= spawnDuration)
            {
                Instantiate(spawner, transform.position, transform.rotation);
                spawnDurationTimer = 0f;
                spawnerTimer = 0f;
                chargeMove.enabled = true;
                hp.setInvincible(false);
                shield.SetActive(false);
            }
        }
    }

    private void Phase2()
    {
        ///Reseting Phases
        if (anim.GetInteger("Phase") < 2)
        {
            anim.SetInteger("Phase", 2);
            return;
        }
        if (anim.GetInteger("Phase") == 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("Comedic Pause"))
        {
            hp.setInvincible(true);
            chargeMove.enabled = false;
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Phase2"))
        {
            chargeMove.enabled = true;
            hp.setInvincible(false);
        }

        ///Bullet Hell
        if (chargeMove.getNumTimesCharged() > numCharges)
        {
            anim.SetInteger("Phase", 3);
            chargeMove.enabled = false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hell"))
                HellTime();
        }
    }

    #region Phase 2
    private void HellTime()
    {
        ///The main function that controls the bullet hells and which one happens
        hellTimer += Time.deltaTime;

        ///Setting up your own personal hell
        switch (currentHell)
        {
            case LevelsOFHell.Basic:
                basicHell(1, 4); ///x1 speed, 4 guns
                break;
            case LevelsOFHell.Wiggly:
                wigglyHell(1, 4); ///x1 speed, 4 guns
                break;
            case LevelsOFHell.Pulse:
                pulsingHell(1); ///1 sec between pulses
                break;
            case LevelsOFHell.HalfCirc:
                halfCircleHell(1);
                break;
            case LevelsOFHell.Arc:
                arcHell(1, 3);
                break;
            case LevelsOFHell.AimedArc:
                aimedArcHell(0.5f, 3);
                break;
            case LevelsOFHell.FrontBlast:
                frontBlastHell(3);
                break;
            case LevelsOFHell.HarderBasic:
                basicHell(2, 8);
                break;
            case LevelsOFHell.HarderPulse:
                pulsingHell(0.5f);
                break;
            case LevelsOFHell.HarderWiggly:
                wigglyHell(2, 8);
                break;
            default:
                basicHell(1, 4);
                break;
        }

        if (hellTimer >= bulletHellDuration)
        {
            resetForNextPart();
        }
    }

    #region Levels of Hell
    private void basicHell(float speed, int guns)
    {
        ///A replica of the SlowRotation one that Dom did
        gunHolderAnim.SetInteger("Phase", 1); ///basic turning
        gunHolderAnim.speed = speed; 
        Shoot(guns);
    }
    
    private void wigglyHell(float speed, int guns)
    {
        ///A replica of the 90DegreeWiggle that Dom did
        ///Applicable stages: 1,2,4
        gunHolderAnim.SetInteger("Phase", 2); ///wiggly
        gunHolderAnim.speed = speed;
        Shoot(guns);
    }

    private void pulsingHell(float pulseRate)
    {
        ///Bullets "pulse" out in a circle like waves
        ///fastest suggested pulseRate is 0.03, otherwise it lags
        gunHolderAnim.SetInteger("Phase", 0); ///no extra movement
        Vector3[] allDirections = getDirections(16);
        pulsePattern(pulseRate, allDirections);
    }

    private void halfCircleHell(float pulseRate)
    {
        gunHolderAnim.SetInteger("Phase", 0); ///no extra movement
        Vector3[] halfCircDirections = getArcDirections(5);
        pulsePattern(pulseRate, halfCircDirections);
    }

    private void arcHell(float pulseRate, int width)
    {
        gunHolderAnim.SetInteger("Phase", 0); ///no extra movement
        Vector3[] arcDirections = getArcDirections(width);
        pulsePattern(pulseRate, arcDirections);
    }

    private void aimedArcHell(float pulseRate, int width)
    {
        gunHolder.transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        arcHell(pulseRate, width);
    }

    private void tighterArcHell(float pulseRate, int width, int fillNum)
    {
        gunHolderAnim.SetInteger("Phase", 0); ///no extra movement

        //insert interpolation here
        //pulsePattern(pulseRate, directions);
    }

    private void frontBlastHell(int width)
    {
        gunHolderAnim.SetInteger("Phase", 0); ///no extra movement
        Vector3[] arcDirections = getArcDirections(width);
        ShootAt(arcDirections);
    }
    
    private void aimedFrontBlastHell(int width)
    {
        gunHolder.transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        frontBlastHell(width);
    }
    #endregion


    #region Bullet Spawning Stuff
    private void Shoot(int numGuns)
    {
        ///Shoots evenly spaced guns
        ///numGuns allowed: 1,2,4,8,16
        Vector3[] allDirections = getDirections(numGuns);
        ShootAt(allDirections);
    }

    private void ShootAt(Vector3[] directions)
    {
        ///Shoots in the given directions based on the fireTimer
        fireTimer -= Time.deltaTime;

        /// Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            foreach (Vector3 direction in directions)
            {
                instantiateBullet(direction);
            }
            fireTimer = setFireTimer;
        }
    }

    private void pulsePattern(float pulseRate, Vector3[] allDirections)
    {
        ///Instead of the regular fire, fires a bunch in patterned bursts
        ///The patterns depend on the directions
        pulseTimer += Time.deltaTime;

        if (pulseTimer >= pulseRate)
        {
            foreach (Vector3 direction in allDirections)
            {
                instantiateBullet(direction);
            }
            pulseTimer = 0;
        }
    }

    private void instantiateBullet(Vector3 direction)
    {
        ///Instantiated a bullet that will shoot in the specified direction
        GameObject newBullet = Instantiate(bullet, gunHolder.transform.position, Quaternion.LookRotation(Vector3.forward, direction));
        newBullet.tag = "Enemy Bullet";
    }
    #endregion

    #region Direction Stuff
    private Vector3[] getDirections(int numDirections)
    {
        ///Will get the directions based off gunHolder
        ///Possible total directions: 1,2,4,8,16
        
        Vector3[] allDirections = new Vector3[numDirections];

        Transform gunPlace = gunHolder.transform;
        if (numDirections >= 1)
            allDirections[0] = gunPlace.up; ///up, 90deg
        if (numDirections >= 2)
            allDirections[1] = -gunPlace.up; ///down, 270deg
        if (numDirections >= 4)
        {
            allDirections[2] = -gunPlace.right; ///left, 180deg
            allDirections[3] = gunPlace.right; ///right, 0deg
        }
        if (numDirections >= 8)
        {
            allDirections[4] = allDirections[0] + allDirections[3]; /// 45deg
            allDirections[5] = allDirections[0] + allDirections[2]; /// 135deg
            allDirections[6] = allDirections[1] + allDirections[2]; /// 225deg
            allDirections[7] = allDirections[1] + allDirections[3]; /// 315deg
        }
        if (numDirections == 16) ///////FIX
        {
            ///top right
            allDirections[8] = allDirections[4] + allDirections[0];
            allDirections[9] = allDirections[4] + allDirections[3];

            ///top left
            allDirections[10] = allDirections[5] + allDirections[0];
            allDirections[11] = allDirections[5] + allDirections[2];

            ///bottom left
            allDirections[12] = allDirections[6] + allDirections[1];
            allDirections[13] = allDirections[6] + allDirections[2];

            ///bottom right
            allDirections[14] = allDirections[7] + allDirections[1];
            allDirections[15] = allDirections[7] + allDirections[3];
        }
        return allDirections;
    }

    private Vector3[] getArcDirections(int width)
    {
        ///Returns (2*width - 1) directions that form an arc
        ///but with a max of 8, for a num_dir of 15
        Vector3[] allDirections = getDirections(16); /// as much of the circle as feasible imo

        int num_dir = (2 * width) - 1;
        Vector3[] arc = new Vector3[num_dir];

        if (num_dir >= 1)
            arc[0] = allDirections[0]; ///forward, center
        if (num_dir >= 3)
        {
            arc[1] = allDirections[8];
            arc[2] = allDirections[10];
        }
        if (num_dir >= 5)
        {
            arc[3] = allDirections[4];
            arc[4] = allDirections[5];
        }
        if (num_dir >= 7)
        {
            arc[5] = allDirections[9];
            arc[6] = allDirections[11];
        }
        if (num_dir >= 9)
        {
            arc[7] = allDirections[3];
            arc[8] = allDirections[2];
        }
        if (num_dir >= 11)
        {
            arc[9] = -arc[6];
            arc[10] = -arc[5];
        }
        if (num_dir >= 13)
        {
            arc[11] = -arc[4];
            arc[12] = -arc[3];
        }
        if (num_dir == 15)
        {
            arc[13] = -arc[2];
            arc[14] = -arc[1];
        }
        return arc;
    }
    #endregion

    #region Phase stuff
    private void resetForNextPart()
    {
        hellTimer = 0;
        chargeMove.resetNumTimesCharged();
        chargeMove.cancelCharge();

        gunHolderAnim.SetInteger("Phase", 0);
        setNextPhase();
        anim.SetInteger("Phase", 4);
    }

    private void setNextPhase()
    {
        if (demo)
            setDemoPhases();
        else
            setActualPhases();
    }

    private void setActualPhases()
    {
        switch (currentHell)
        {
            case LevelsOFHell.Basic:
                currentHell = LevelsOFHell.Wiggly;
                break;
            case LevelsOFHell.Wiggly:
                currentHell = LevelsOFHell.Pulse;
                break;
            case LevelsOFHell.Pulse:
                currentHell = LevelsOFHell.HalfCirc;
                break;
            case LevelsOFHell.HalfCirc:
                currentHell = LevelsOFHell.AimedArc;
                break;
            case LevelsOFHell.AimedArc:
                currentHell = LevelsOFHell.FrontBlast;
                break;
            case LevelsOFHell.FrontBlast:
                currentHell = LevelsOFHell.Basic;
                break;
            default:
                currentHell = LevelsOFHell.Basic;
                break;
        }
    }

    private void setDemoPhases()
    {
        switch (currentHell)
        {
            case LevelsOFHell.Basic:
                Debug.Log("Mode Set To: HARDER BASIC");
                currentHell = LevelsOFHell.HarderBasic;
                break;
            case LevelsOFHell.HarderBasic:
                Debug.Log("Mode Set To: WIGGLY");
                currentHell = LevelsOFHell.Wiggly;
                break;
            case LevelsOFHell.Wiggly:
                Debug.Log("Mode Set To: HARDER WIGGLY");
                currentHell = LevelsOFHell.HarderWiggly;
                break;
            case LevelsOFHell.HarderWiggly:
                Debug.Log("Mode Set To: PULSE");
                currentHell = LevelsOFHell.Pulse;
                break;
            case LevelsOFHell.Pulse:
                Debug.Log("Mode Set To: HARDER PULSE");
                currentHell = LevelsOFHell.HarderPulse;
                break;
            case LevelsOFHell.HarderPulse:
                Debug.Log("Mode Set To: HALF CIRC");
                currentHell = LevelsOFHell.HalfCirc;
                break;
            case LevelsOFHell.HalfCirc:
                Debug.Log("Mode Set To: ARC");
                currentHell = LevelsOFHell.Arc;
                break;
            case LevelsOFHell.Arc:
                Debug.Log("Mode Set To: ANIMED ARC");
                currentHell = LevelsOFHell.AimedArc;
                break;
            case LevelsOFHell.AimedArc:
                Debug.Log("Mode Set To: FRONT BLAST");
                currentHell = LevelsOFHell.FrontBlast;
                break;
            case LevelsOFHell.FrontBlast:
                Debug.Log("Mode Set To: AIMED FRONT BLAST");
                currentHell = LevelsOFHell.AimedFrontBlast;
                break;
            case LevelsOFHell.AimedFrontBlast:
                Debug.Log("Mode Set To: BASIC");
                currentHell = LevelsOFHell.Basic;
                break;
            default:
                Debug.Log("Mode Set To: BASIC");
                currentHell = LevelsOFHell.Basic;
                break;
        }
    }
    #endregion
    #endregion
}
