INCLUDE ../../global.ink
VAR TalkedTo_CatCultist = false
VAR ReleasedCat = false
VAR CatRanAway = false
VAR Resolved_CatCultist = false


~ReleasedCat = ExecuteQuery("GetGameSwitchState", "ReleasedCat")
~TalkedTo_CatCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_CatCultist")
~CatRanAway = ExecuteQuery("GetGameSwitchState", "CatRanAway")
~Resolved_CatCultist = ExecuteQuery("GetGameSwitchState", "Resolved_CatCultist")


{
	- Resolved_CatCultist:
	    -> resolved_quest
	- ReleasedCat:
		->released_cat
	- TalkedTo_CatCultist:
		-> after_first_talk
	- else:
		-> first_talk
}

=== first_talk ===
It's just terrible, isn't it? #Speaker_Default
Yes, we really can't afford to not perform that ritual tonight. I will do my best to - #Speaker_Player
What!? No! What do I care about the stupid ritual. What was that about anyway? #Speaker_Default
Well, summoning ...something ...probably #Speaker_Player
Yeah, see? Nobody knows. #Speaker_Default
And how could anyone care too, now that my poor cat ist missing.
... right. Your cat. That sure sounds more important. #Speaker_Player
Exactly!! The second leader took her! I just know it! Wouldn't be the first time he layed hands on her.. #Speaker_Default
Sniff... My poor baby! Just where could she be? 
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_CatCultist")
I... better get going. #Speaker_Player
->END

=== after_first_talk ====
Sniff... My poor baby! Just where could she be?
->DONE

// Quest resolution
=== released_cat ===
I got your cat.
(place cat into scene)
Are you insane?? That's not my cat!
This place is really something! First that Shopkeeper's son asks me if I have a cat hair to spare and now you bring me this dirty stray.
What's wrong with you all?
(Cat runs away)
~ExecuteAction("SetGameSwitchTrue", "CatRanAway")
See!? My darling would never take off like that before kissing me goodbye.
Where did that filthy stray run off to anyway?
Oh this is all too much.
~ExecuteAction("SetGameSwitchTrue", "Resolved_CatCultist")
Please excuse me, I have to lie down and cry now.
-> END

=== resolved_quest ===
(excessive sobbing)
-> DONE