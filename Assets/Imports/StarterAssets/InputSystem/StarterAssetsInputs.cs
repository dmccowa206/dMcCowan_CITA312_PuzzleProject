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
		public Vector2 point;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool pause;
		public bool click;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

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

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }
		public void OnPause(InputValue value)
        {
            PauseInput(value.isPressed);
        }
		public void OnPoint(InputValue value)
        {
            if (!cursorInputForLook)
            {
                PointInput(value.Get<Vector2>());
            }
        }
		public void OnClick(InputValue value)
        {
            if (!cursorInputForLook)
            {
                ClickInput(value.isPressed);
            }
        }
		public void EnableMouseLook(bool toggle)
		{
			cursorLocked = toggle;
			cursorInputForLook = toggle;
			if (!cursorInputForLook)
            {
                LookInput(Vector2.zero);
				SetCursorState(toggle);
            }
			else
            {
				SetCursorState(toggle);
            }
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

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }
		public void PauseInput(bool newPauseState)
        {
            pause = newPauseState;
        }
		public void PointInput(Vector2 pointlocation)
        {
            point = pointlocation;
        }
		public void ClickInput(bool newClickState)
        {
            click = newClickState;
        }
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}