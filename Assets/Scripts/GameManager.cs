using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public UIController UIC;
    public CameraShaker CS;
    public HoleController HC;
    public StageTransitionHandler STH;

    public Transform StageTransitionStartTransform, StageTransitionTargetTransform;
    public Transform CameraSecondStageTransform;

    public GameObject[] FirstHalfCubeParents, SecondHalfCubeParents;

    public DeathWall[] DeathWalls;

    private Action _scoreAction, _deathAction;

    private int[] _cubeCounts;
    private int[] _destroyedCubes;

    // First half or second half
    private int _currentLevelPart;
    
    private bool _startPanelHidden;
    private bool _gameOver, _stageTransitionActive, _levelComplete;

    private void Awake(){
        _cubeCounts = new int[2];
        _destroyedCubes = new int[2];

        _cubeCounts[0] = FindCubeCount(FirstHalfCubeParents);
        _cubeCounts[1] = FindCubeCount(SecondHalfCubeParents);

        _destroyedCubes[0] = 0;
        _destroyedCubes[1] = 0;

        _deathAction += GameOver;
        _scoreAction += PlayerScored;

        for(int i = 0; i < DeathWalls.Length; i++){
            DeathWalls[i].AssignCallbacks(_scoreAction, _deathAction);
        }

        _currentLevelPart = 0;

        UIC.ResetBridges();
        UIC.SetLevelTexts(1);
        HC.SetInput(true);

        CS.AssignCameraTransform(Camera.main.transform);
    }

    private void Update(){
        if(_stageTransitionActive){
            return;
        }

        if(_levelComplete){
            if (Input.GetMouseButtonDown(0))
            {
                if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }else{
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                return;
            }
        }

        if(_gameOver){
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        if(!_startPanelHidden){
            if(Input.GetMouseButton(0)){
                UIC.HideStartPanel();
                _startPanelHidden = true;
            }
        }
    }

    private int FindCubeCount(GameObject[] p_parents){
        int cubeCount = 0;

        for (int i = 0; i < p_parents.Length; i++)
        {
            cubeCount += p_parents[i].transform.childCount;
        }

        return cubeCount;
    }

    private void PlayerScored(){
        _destroyedCubes[_currentLevelPart]++;

        UIC.SetBridgeFill(_currentLevelPart, _destroyedCubes[_currentLevelPart] / (float)_cubeCounts[_currentLevelPart]);

        if(_destroyedCubes[_currentLevelPart] == _cubeCounts[_currentLevelPart]){
            if (_currentLevelPart == 1){
                LevelComplete();
                return;
            }

            _stageTransitionActive = true;
            HC.SetInput(false);

            STH.StartTransition(HC.transform, StageTransitionStartTransform.position, StageTransitionTargetTransform.position, () => {StageTransitionComplete();});
            STH.StartTransition(Camera.main.transform, Camera.main.transform.position, CameraSecondStageTransform.position, null);
        }
    }

    private void LevelComplete(){
        UIC.LevelComplete();
        HC.SetInput(false);

        _levelComplete = true;
    }

    private void GameOver(){
        //Debug.Log("Game over");
        UIC.ShowRestartPanel();
        CS.StartShaking();
        HC.SetInput(false);
        
        _gameOver = true;
    }

    private void StageTransitionComplete(){
        _stageTransitionActive = false;
        _currentLevelPart++;
        HC.SetInput(true);
    }
}
