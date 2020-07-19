using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DifficultyController))]
public class GameManagerEditor : Editor {

    public override void OnInspectorGUI() {

        //base.OnInspectorGUI();

        DifficultyController targetScript = (DifficultyController)target;

        GUIStyle header = new GUIStyle();
        header.fontStyle = FontStyle.Bold;
        header.alignment = TextAnchor.MiddleLeft;

        float horizontalSpace = 10;
        float verticalSpace = 10;
        GUILayoutOption fieldMaxWidth = GUILayout.MaxWidth(120);
        GUILayoutOption headerMaxWidth = GUILayout.MaxWidth(145);

        GUILayout.BeginVertical();

            GUILayout.Space(verticalSpace);
                GUILayout.BeginHorizontal();
                GUILayout.Label(" ", header, headerMaxWidth);
                GUILayout.Label("Initial", header, headerMaxWidth);
                GUILayout.Label("Limit", header, headerMaxWidth);
                GUILayout.Label("Current", header, headerMaxWidth);
            GUILayout.EndHorizontal();

            GUILayout.Label("Asteroid", header);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate");
                targetScript.init_asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.init_asteroidSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.limit_asteroidSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.asteroidSpawnRate, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed");
                targetScript.init_asteroidSpeed = EditorGUILayout.FloatField(targetScript.init_asteroidSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_asteroidSpeed = EditorGUILayout.FloatField(targetScript.limit_asteroidSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.asteroidSpeed = EditorGUILayout.FloatField(targetScript.asteroidSpeed, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(verticalSpace);
            GUILayout.Label("Shooter", header);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate");
                targetScript.init_ShooterSpawnRate = EditorGUILayout.FloatField(targetScript.init_ShooterSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterSpawnRate = EditorGUILayout.FloatField(targetScript.limit_ShooterSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterSpawnRate = EditorGUILayout.FloatField(targetScript.shooterSpawnRate, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed");
                targetScript.init_ShooterSpeed = EditorGUILayout.FloatField(targetScript.init_ShooterSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterSpeed = EditorGUILayout.FloatField(targetScript.limit_ShooterSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterSpeed = EditorGUILayout.FloatField(targetScript.shooterSpeed, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Wait Time");
                targetScript.init_ShooterWaitTime = EditorGUILayout.FloatField(targetScript.init_ShooterWaitTime, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterWaitTime = EditorGUILayout.FloatField(targetScript.limit_ShooterWaitTime, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterWaitTime = EditorGUILayout.FloatField(targetScript.shooterWaitTime, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Time Btw Shoots");
                targetScript.init_ShooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.init_ShooterTimeBtwShoots, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.limit_ShooterTimeBtwShoots, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.shooterTimeBtwShoots, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(verticalSpace);
            GUILayout.Label("Missile", header);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate");
                targetScript.init_MissileSpawnRate = EditorGUILayout.FloatField(targetScript.init_MissileSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_MissileSpawnRate = EditorGUILayout.FloatField(targetScript.limit_MissileSpawnRate, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.missileSpawnRate = EditorGUILayout.FloatField(targetScript.missileSpawnRate, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed");
                targetScript.init_MissileSpeed = EditorGUILayout.FloatField(targetScript.init_MissileSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_MissileSpeed = EditorGUILayout.FloatField(targetScript.limit_MissileSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.missileSpeed = EditorGUILayout.FloatField(targetScript.missileSpeed, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(verticalSpace);
            GUILayout.Label("Guided Missile", header);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Timer");
                targetScript.init_GuidedMissileTimer = EditorGUILayout.FloatField(targetScript.init_GuidedMissileTimer, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_GuidedMissileTimer = EditorGUILayout.FloatField(targetScript.limit_GuidedMissileTimer, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.guidedMissileTimer = EditorGUILayout.FloatField(targetScript.guidedMissileTimer, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed");
                targetScript.init_GuidedMissileSpeed = EditorGUILayout.FloatField(targetScript.init_GuidedMissileSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_GuidedMissileSpeed = EditorGUILayout.FloatField(targetScript.limit_GuidedMissileSpeed, fieldMaxWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.guidedMissileSpeed = EditorGUILayout.FloatField(targetScript.guidedMissileSpeed, header, fieldMaxWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

        GUILayout.Space(verticalSpace);
        GUILayout.EndVertical();
    }
}
