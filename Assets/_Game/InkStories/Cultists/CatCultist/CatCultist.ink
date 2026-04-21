INCLUDE ../../global.ink
VAR talkedTo_catCultist = false
VAR releasedCat = false
VAR catRanAway = false

~releasedCat = ExecuteQuery("GetGameSwitchState", "ReleasedCat")
~talkedTo_catCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_CatCultist")
~catRanAway = ExecuteQuery("GetGameSwitchState", "CatRanAway")


{
	- releasedCat:
		->released_cat
	- talkedTo_catCultist:
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
This place is really something! First the Shopkeeper's assistant asks me if I have a cat hair to spare and now you bring me this dirty stray.
Is there really no one here who sees what's really important!?
(Cat runs away)
~ExecuteAction("SetGameSwitchTrue", "CatRanAway")
See!? My darling would never take off like that before kissing me goodbye.
-> END