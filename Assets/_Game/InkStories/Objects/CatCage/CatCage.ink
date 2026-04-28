INCLUDE ../../global.ink
VAR GotCageKey = true

// Not in MVP
//~GotCageKey = ExecuteQuery("GetGameSwitchState", "GotCageKey")

{
	- GotCageKey:
		-> got_cage_key
	- else:
		-> no_cage_key
}

=== no_cage_key ===
The door is locked.
->DONE

=== got_cage_key ====
The door is closed, but not locked.
Someone might be looking for that cat. Better take it!
->DONE