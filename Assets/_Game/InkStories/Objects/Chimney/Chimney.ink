INCLUDE ../../global.ink
VAR Resolved_CatCultist = false
VAR GotItem_CleaningWaterInAJug = false

~Resolved_CatCultist = ExecuteQuery("GetGameSwitchState", "Resolved_CatCultist")
~GotItem_CleaningWaterInAJug = ExecuteQuery("GetGameSwitchState", "GotItem_CleaningWaterInAJug")

{
	- GotItem_CleaningWaterInAJug:
		-> open_secret_pathway
	- Resolved_CatCultist:
		-> cat_in_front_of_chimney
	- else:
		-> default
}

=== default ===
It's a chimney. The flames are burning hot, better not get too close!
->DONE

=== cat_in_front_of_chimney ===
You follow the eyes of the cat and make out a switch in the back.
The flames burn too hot to be able to reach it.
~ExecuteAction("SetGameSwitchTrue", "InvestigatedChimney")
If you only had something to put out the flames..
->DONE

=== open_secret_pathway ====
You pull the water filled mug from your inventory and empty it over the flames.
They die out instantly. The wood seems unusually... clean... all of a sudden.
~ExecuteAction("SetGameSwitchTrue", "UnlockedRoomBehindChimney")
Eagerly you press the switch on the backside..
->DONE
