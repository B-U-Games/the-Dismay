using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool interact;
		public bool pause;
		public bool flashlight;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private float _timer = 0f;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
		    AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
		    ShootInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
		    InteractInput(value.isPressed);
		}

		public void OnPause(InputValue value)
		{
		    PauseInput(value.isPressed);
		}

		public void OnFlashlight(InputValue value)
		{
		    FlashlightInput(value.isPressed);
		}
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState && !aim;
		}

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
			sprint = sprint && !aim;
        }

		public void ShootInput(bool newShootState)
		{
			if (_timer <= 0)
			{
				shoot = newShootState && aim;
				_timer = 1f;
			}
		}

        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }

        public void PauseInput(bool newPauseState)
        {
			if (newPauseState)
			{
				pause = !pause;
			}
        }

        public void FlashlightInput(bool newFlashlightState)
        {
            if (newFlashlightState)
            {
                flashlight = !flashlight;
                GetComponent<ThirdPersonShooterController>().ToggleFlashlight();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void Update()
		{
			if (_timer > 0)
			{
				_timer -= Time.deltaTime;
			}
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}