INCLUDE ../../global.ink
VAR gotItem_beer = false
VAR talkedTo_partyCultist = false

~gotItem_beer = ExecuteQuery("GetGameSwitchState", "GotItem_Beer")
~talkedTo_partyCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_PartyCultist")

{
	- gotItem_beer:
		->got_beer
	- talkedTo_partyCultist:
		-> after_first_talk
	- else:
		-> first_talk
}

=== first_talk ===
Yooo! It's ma boy!
I don't think we met before.. # Speaker_Player
Hahaha! Yes! YES! These are exactly the kind of jokes why I consider you my best friend! Classic! #Speaker_Default
Could you get me another beer, pal?
The Shopkeeper kinda refuses me.
The grand leader is dead and you are celebrating? #Speaker_Player
Oh shit, she's WHAT!? Well, that kinda explains the bad vibes around here. #Speaker_Default
...
Wait!
I think I remember something!
No wait, I don't.
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_PartyCultist")
Man I'm so thirsty... that damn shopkeeper!
->END

=== after_first_talk ====
Man I'm so thirsty... that damn shopkeeper!
->DONE

// Quest resolution
=== got_beer ===
Yooo! It's ma boy!
And my beer!!
(gulp gulp gulp)
Satan's grace that's so much better.
Woah! Who are you!?
-> END