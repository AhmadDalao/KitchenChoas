using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObject {

    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerPickedSomethingEvent;

    public event EventHandler<CounterSelectVisualEventArgs> CounterSelectVisualEvent;
    public class CounterSelectVisualEventArgs : EventArgs {
        public BaseCounter BaseCounterSelectVisual;
    }

    [SerializeField] private GameInputManager _gameInputManager;
    [SerializeField] private LayerMask _layerMask;


    [SerializeField] private Transform _spawnLocation;


    private KitchenObject _kitchenObject;


    [Header("Float")]
    private float _moveSpeed = 8f;
    private float _moveDistance;
    private bool _isMoving;

    [Header("Vector3")]
    private Vector3 _lastMoveDirection;

    [Header("ClearCounter")]
    private BaseCounter _baseCounter;




    private void Awake() {


        if (Instance != null) {
            Debug.Log("there is more than 1 player Instance");
        }
        Instance = this;

    }


    private void Start() {

        _gameInputManager.InteractEvent += _gameInputManager_InteractEvent;
        _gameInputManager.InteractCuttingEvent += _gameInputManager_InteractCuttingEvent;
    }


    private void _gameInputManager_InteractCuttingEvent(object sender, EventArgs e) {
        // if game state is over than playing return prevent the player from interacting
        if (!GameManager.Instance.IsPlayingTimeState()) return;

        if (_baseCounter != null) {
            _baseCounter.InteractCutting(this);
        }

    }

    private void _gameInputManager_InteractEvent(object sender, EventArgs e) {
        // if game state is over than playing return prevent the player from interacting
        if (!GameManager.Instance.IsPlayingTimeState()) return;

        if (_baseCounter != null) {
            _baseCounter.Interact(this);
        }

    }

    private void Update() {

        HandlePlayerMovement();
        HandleInteraction();
    }


    private void HandlePlayerMovement() {
        _moveDistance = _moveSpeed * Time.deltaTime;

        Vector2 userInput = _gameInputManager.GetPlayerMovementVector2Normalized();
        Vector3 moveDirection = new Vector3(userInput.x, 0f, userInput.y);

        float playerHight = 2f;
        float playerRadius = 0.7f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirection, _moveDistance);

        if (!canMove) {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f);

            canMove = moveDirectionX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirectionX, _moveDistance);

            if (canMove) {
                moveDirection = moveDirectionX;
            } else {

                Vector3 moveDirectionZ = new Vector3(moveDirection.x, 0f, 0f);

                canMove = moveDirectionZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirectionZ, _moveDistance);

                if (canMove) {
                    moveDirection = moveDirectionZ;
                } else {
                    Debug.Log("player can't move here");
                }
            }
        }



        _isMoving = moveDirection != Vector3.zero;

        if (_isMoving) {
            float rotateSpeed = 12f * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed);
        }

        if (canMove) {
            transform.position += moveDirection * _moveDistance;
        }

    }

    private void HandleInteraction() {

        Vector2 userInput = _gameInputManager.GetPlayerMovementVector2Normalized();
        Vector3 moveDirection = new Vector3(userInput.x, 0f, userInput.y);

        if (moveDirection != Vector3.zero) {
            _lastMoveDirection = moveDirection;
        }

        float maxDistance = 2f;

        if (Physics.Raycast(transform.position, _lastMoveDirection, out RaycastHit raycastHit, maxDistance, _layerMask)) {

            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter)) {

                if (_baseCounter != baseCounter) {

                    HandleClearCounterSelectVisual(baseCounter);
                }
            } else {
                HandleClearCounterSelectVisual(null);
            }

        } else {
            HandleClearCounterSelectVisual(null);
        }
    }

    private void HandleClearCounterSelectVisual(BaseCounter baseCounter) {
        this._baseCounter = baseCounter;
        CounterSelectVisualEvent?.Invoke(this, new CounterSelectVisualEventArgs {
            BaseCounterSelectVisual = _baseCounter
        });
    }

    public bool GetIsMoving() {
        return _isMoving;
    }


    public Transform GetTransformToFollowParent() {
        return _spawnLocation;
    }

    public void ClearKitchenObject() {
        this._kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this._kitchenObject = kitchenObject;

        if (_kitchenObject != null) {
            OnPlayerPickedSomethingEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return this._kitchenObject;
    }

    public bool HasKitchenObject() {
        return this._kitchenObject != null;
    }


}
