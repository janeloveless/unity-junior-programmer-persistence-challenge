# junior-programmer-unity-persistence-challenge

This is my solution to the Data persistence challenge in the Unity Learn Junior Programming pathway.

# Challenge

In brief, the challenge involved taking a pre-made game in the style of breakout
(an arcade classic), adding a menu screen on which a player could enter their
name, and implementing a hi-score feature which tracks the higher scoring player
(and their score) over multiple games and multiple sessions.

Here is a link to more details on the challenge :
 
https://learn.unity.com/tutorial/submission-data-persistence-in-a-new-repo


# Solution

My solution involved implementing a simple menu scene with player name input,
start and exit buttons, and a button to reset the current hi-score to zero, and
implementing a HighScore singleton class to keep track of the hi-score both
during and between games. 

The singleton HighScore object persists between the menu scene and the main
gameplay scene, and exposes methods to check a challenger's score against the
currently registered hi-score (and update the registered score accordingly) and
to save the current hi-score to disk. 

I edited the existing MainManager class, which keeps track of game state,
including a player's current score, to challenge the current hi-score whenever
the player scores a point. The MainManager class also asks HighScore to save the
current hi-score on game over. Performance could be improved by only saving if
the hi-score has been updated during the game, but given we write only a small
amount of data to disk on each game over, my current solution shouldn't be too
performance heavy and it doesn't negatively effect gameplay.

Finally, when the HighScore singleton is instantiated on starting the game, the
last hi-score is loaded from disk. The case where there is no hi-score saved
(for instance on the first run of the game) is handled by setting the score to
zero.