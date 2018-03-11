using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strings {

	public static string[] Mayhoc1 = new string[] {
		"New eyes... A mouth...",
		"So it's true.",
		"The Rogue Gem does grant the power of evolution.",
		"But be warned, little slime.",
		"You may have gained some new skills, but it comes at a price.",
		"Other creatures who covet this power will not rest until they've seized it from you.",
		"You better learn to master those new skills soon...",
		"...Or you'll find yourself dead.",

	};

	public static string[] Mayhoc2a = new string[] {
		"So, you made it.",
		"To think a mere slime could have come this far.",
		"The Rogue Gem is truly powerful indeed."
	};

	public static string[] Mayhoc2b = new string[] {
		"It irks me to think that after years of searching for the gem, I was bested by a little slime!",
		"The Rogue Gem is named after The Rogue herself. She, who had the ability to absorb powers.",
		"It grants its beholder the same powers.",
		"When you found the gem first, I decided to put those powers to the test.",
		"And sent my minions after you.",
		"Now that I've seen its true power...",
		"...I'm going to make it MINE!"
	};

	public static IDictionary<string, string[]> dialogueMap = new Dictionary<string, string[]> {
		{"boss1", Mayhoc1},
		{"boss2a", Mayhoc2a},
		{"boss2b", Mayhoc2b},
	};
		
}
