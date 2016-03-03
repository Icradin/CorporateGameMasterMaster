using UnityEngine;
using System.Collections;

public class cursor_test_class : MonoBehaviour {

	private bool is_locked;

    void Start()
    {
		//ToggleCursorState();
       
    }

    void Update()
    {
		//CheckForInput();
		CheckIfCursorShouldBeLocked();
    }

    public void ToggleCursorState()
    {
        is_locked = !is_locked;
    }

   // public void CheckForInput ()
   // {
   //     if (!is_locked)
   //     {
			//ToggleCursorState();
   //     }
   // }

    void CheckIfCursorShouldBeLocked()
    {
        if (is_locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
