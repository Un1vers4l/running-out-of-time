INCLUDE ../../global.ink
VAR TalkedTo_DeathWishCultist = false
VAR TalkedTo_CleaningCrew = false
VAR DeathWishCultistReadyForSacrifice = false
VAR DeathWishCultistDead = false

~TalkedTo_DeathWishCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_DeathWishCultist")
~TalkedTo_CleaningCrew = ExecuteQuery("GetGameSwitchState", "TalkedTo_CleaningCrew")
~DeathWishCultistReadyForSacrifice = ExecuteQuery("GetGameSwitchState", "DeathWishCultistReadyForSacrifice")
~DeathWishCultistDead = ExecuteQuery("GetGameSwitchState", "DeathWishCultistDead")

{
	- DeathWishCultistDead:
	    -> corpse_text
	- DeathWishCultistReadyForSacrifice:
		->while_being_sacrificed
	- TalkedTo_CleaningCrew:
		-> tell_him_to_get_ready
	- TalkedTo_DeathWishCultist:
		-> pre_talk_to_cleaning_crew
	- else:
		-> first_talk
}

=== first_talk ===
Oh man I SO want to be sacrificed! #Speaker_Default
Wait.. you actually WANT to die? #Speaker_Player
Of course! Isn't that what every good cultist strives for? Violently getting stabbed with a dagger in service of the dark lord? #Speaker_Default
Uuhh.. no? #Speaker_Player
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_DeathWishCultist")
Nothing would bring me greater joy than being killed. If you hear of someone needing a sacrifice, PLEASE think of me. #Speaker_Default
->END

=== pre_talk_to_cleaning_crew ===
I really really hope to be stabbed soon! 
->END

=== tell_him_to_get_ready ====
Soooo, you still want to be dead? #Speaker_Player
Yes of course!! Have you found someone who wants to sacrifice me? Don't tease me like this if you haven't. #Speaker_Default
# Speaker_Player // also needed so that line before is rendered because of choices
* [Yes! I need a distraction to lure the cleaning crew away from their water] 
    The dark lord appointed me to fulfill the grand ritual tonight. 
    Your sacrifice is needed to... grant me the means to do so.
    ->tell_him_to_get_ready.cultist_answer_to_offer
* [The dark lord requests your service]
    The dark lord himself asked me to perform a sacrifice immediately. He says it's really important. 
    ->tell_him_to_get_ready.cultist_answer_to_offer

= cultist_answer_to_offer
The dark lord himself!?? #Speaker_Default
Oh my oh my oh my it's actually happening.
Okay okay don't freak out now. We practiced this.
Meet me at the sacrificial table! I will be ready!
...
~ExecuteAction("SetGameSwitchTrue", "DeathWishCultistReadyForSacrifice")
Thank you. 
->DONE

// Quest resolution
=== while_being_sacrificed ===
// Maybe small cutscene?
~ExecuteAction("SetGameSwitchTrue", "DeathWishCultistDead")
-> END

=== corpse_text ===
Good for him. Someone better clean this up though!
-> DONE