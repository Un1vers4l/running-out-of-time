INCLUDE ../../global.ink
VAR DeathWishCultistDead = false
VAR Got_Item_EmptyBeerJug = false
VAR BroughtLetterToLover = false

~DeathWishCultistDead = ExecuteQuery("GetGameSwitchState", "TalkedTo_ShopAssistant")
~Got_Item_EmptyBeerJug = ExecuteQuery("GetGameSwitchState", "GotItem_LetterForLover")


{
	- DeathWishCultistDead && Got_Item_EmptyBeerJug:
	    -> can_take_water
	- DeathWishCultistDead:
		-> no_crew_no_jug
	- else:
		-> default
}

=== default ===
The cleaning crew is watching. Better not to risk anything. #Speaker_None
->DONE

=== no_crew_no_jug ==== 
Perfect, no one is watching. #Speaker_None
...but you have nothing to fill the water in!
->DONE

// Quest resolution
=== can_take_water ===
You carefully watch your soroundings and pull the empty mug from your inventory. #Speaker_None
~ExecuteAction("SetGameSwitchTrue", "GotItem_CleaningWaterInAJug")
You fill the mug and slide it back to your belongings. Better not get greedy!
-> END