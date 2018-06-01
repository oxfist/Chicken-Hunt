using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameplayConfiguration : ScriptableObject {
    public float bulletSpeed = 3f;
    public float enemySpeed = 2f;
    public float playerSpeed = 10f;
    public float cameraSmoothSpeed = 0.05f;
    public float cameraOffsetTimer = 1.5f;
    public int initialHP = 10;
    public int bulletDamage = 1;
    public int playerIFrames = 60;
    [HideInInspector] public readonly string[] levelCompleteMessages = {
        "You didn't get any food for your people. You are a disgrace.",
        "You barely managed to get food for your people. Several deaths " +
        "ocurred, including your smallest child, who died of hunger. Your  " +
        "family is devastated. You should try harder next time, it's a " +
        "matter of life and death after all.",
        "You managed to get a decent amount of food, but not enough. Half " +
        "of your people died. Perfect balance was achieved.",
        "You got back to your people with enough food to survive for some " +
        "time, although you must be back to the town to get more before " +
        "hunger destroys your people.",
        "You fulfilled your purpose and managed to get enough food for all " +
        "your people, but now the townsfolk will starve. They're coming for " +
        "you and your family. Brace yourself."
    };

    private readonly float defaultBulletSpeed = 3f;
    private readonly float defaultEnemySpeed = 2f;
    private readonly float defaultPlayerSpeed = 10f;
    private readonly float defaultSmoothSpeed = 0.05f;
    private readonly float defaultCameraOffsetTimer = 1.5f;
    private readonly int defaultInitialHP = 10;
    private readonly int defaultBulletDamage = 1;
    private readonly int defaultPlayerIFrames = 60;

    public void ResetParameters() {
        bulletSpeed = defaultBulletSpeed;
        enemySpeed = defaultEnemySpeed;
        playerSpeed = defaultPlayerSpeed;
        cameraSmoothSpeed = defaultSmoothSpeed;
        cameraOffsetTimer = defaultCameraOffsetTimer;
        initialHP = defaultInitialHP;
        bulletDamage = defaultBulletDamage;
        playerIFrames = defaultPlayerIFrames;
    }
}
