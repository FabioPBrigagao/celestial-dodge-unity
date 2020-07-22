using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DifficultyController))]
public class GameManagerEditor : Editor {

    public override void OnInspectorGUI() {

        //base.OnInspectorGUI();

        DifficultyController targetScript = (DifficultyController)target;

        GUIStyle bold = new GUIStyle();
        bold.fontStyle = FontStyle.Bold;

        float horizontalSpace = 10;
        float verticalSpace = 10;
        GUILayoutOption labelWidth = GUILayout.Width(130);
        GUILayoutOption fieldWidth = GUILayout.Width(120);


        GUILayout.BeginVertical();

            GUILayout.Space(verticalSpace);
                GUILayout.BeginHorizontal();
                GUILayout.Label(" ", bold, labelWidth);
                GUILayout.Label("Initial", bold, labelWidth);
                GUILayout.Label("Limit", bold, labelWidth);
                GUILayout.Label("Current", bold, labelWidth);
            GUILayout.EndHorizontal();

            GUILayout.Label("Asteroid", bold);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate", fieldWidth);
                targetScript.init_asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.init_asteroidSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.limit_asteroidSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.asteroidSpawnRate = EditorGUILayout.FloatField(targetScript.asteroidSpawnRate, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed", fieldWidth);
                targetScript.init_asteroidSpeed = EditorGUILayout.FloatField(targetScript.init_asteroidSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_asteroidSpeed = EditorGUILayout.FloatField(targetScript.limit_asteroidSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.asteroidSpeed = EditorGUILayout.FloatField(targetScript.asteroidSpeed, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(verticalSpace);
            GUILayout.Label("Shooter", bold);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate", fieldWidth);
                targetScript.init_ShooterSpawnRate = EditorGUILayout.FloatField(targetScript.init_ShooterSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterSpawnRate = EditorGUILayout.FloatField(targetScript.limit_ShooterSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterSpawnRate = EditorGUILayout.FloatField(targetScript.shooterSpawnRate, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed", fieldWidth);
                targetScript.init_ShooterSpeed = EditorGUILayout.FloatField(targetScript.init_ShooterSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterSpeed = EditorGUILayout.FloatField(targetScript.limit_ShooterSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterSpeed = EditorGUILayout.FloatField(targetScript.shooterSpeed, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Wait Time", fieldWidth);
                targetScript.init_ShooterWaitTime = EditorGUILayout.FloatField(targetScript.init_ShooterWaitTime, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterWaitTime = EditorGUILayout.FloatField(targetScript.limit_ShooterWaitTime, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterWaitTime = EditorGUILayout.FloatField(targetScript.shooterWaitTime, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Time Btw Shoots", fieldWidth);
                targetScript.init_ShooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.init_ShooterTimeBtwShoots, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_ShooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.limit_ShooterTimeBtwShoots, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.shooterTimeBtwShoots = EditorGUILayout.FloatField(targetScript.shooterTimeBtwShoots, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Health", fieldWidth);
                targetScript.shooterHealth = EditorGUILayout.IntField(targetScript.shooterHealth, fieldWidth);
                GUILayout.Space(horizontalSpace);
            GUILayout.EndHorizontal();

        GUILayout.Space(verticalSpace);
            GUILayout.Label("Missile", bold);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn Rate", fieldWidth);
                targetScript.init_MissileSpawnRate = EditorGUILayout.FloatField(targetScript.init_MissileSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_MissileSpawnRate = EditorGUILayout.FloatField(targetScript.limit_MissileSpawnRate, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.missileSpawnRate = EditorGUILayout.FloatField(targetScript.missileSpawnRate, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed", fieldWidth);
                targetScript.init_MissileSpeed = EditorGUILayout.FloatField(targetScript.init_MissileSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_MissileSpeed = EditorGUILayout.FloatField(targetScript.limit_MissileSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.missileSpeed = EditorGUILayout.FloatField(targetScript.missileSpeed, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(verticalSpace);
            GUILayout.Label("Guided Missile", bold);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Timer", fieldWidth);
                targetScript.init_GuidedMissileTimer = EditorGUILayout.FloatField(targetScript.init_GuidedMissileTimer, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_GuidedMissileTimer = EditorGUILayout.FloatField(targetScript.limit_GuidedMissileTimer, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.guidedMissileTimer = EditorGUILayout.FloatField(targetScript.guidedMissileTimer, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Speed", fieldWidth);
                targetScript.init_GuidedMissileSpeed = EditorGUILayout.FloatField(targetScript.init_GuidedMissileSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                targetScript.limit_GuidedMissileSpeed = EditorGUILayout.FloatField(targetScript.limit_GuidedMissileSpeed, fieldWidth);
                GUILayout.Space(horizontalSpace);
                GUI.enabled = false;
                targetScript.guidedMissileSpeed = EditorGUILayout.FloatField(targetScript.guidedMissileSpeed, bold, fieldWidth);
                GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Health", fieldWidth);
                targetScript.guidedMissileHealth = EditorGUILayout.IntField(targetScript.guidedMissileHealth, fieldWidth);
                GUILayout.Space(horizontalSpace);
            GUILayout.EndHorizontal();

        GUILayout.Space(verticalSpace);
        GUILayout.EndVertical();
    }
}
