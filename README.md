This game is under development, feel free to take a look. Working on fixing the PlayerSteering method which is abit messy atm. 

Runs okay right now though.

The gamepads we are using ( cool [+..••] NES-replicas ) don't map correctly in monogame, so we might fork this project for correct gamepad mapping and keyboard version.

Added keyboard steering:

How to use: Comment out GamePadSteering, and remove comment from TemporaryKeyboardSteering. Warning:  Do not use both of these methods at the same time, it might lag the game abit.

Keyboard mapping: 

Player One:
Left: A, Right: D, Down: S, RotateClockWise: Space, RotateCounterClockwise: Shift.
PlayerTwo:
Left: LeftArrow, Right: RightArrow, Down: DownArrow, RotateClockWise: NUM0, RotateCounterClockwise: Enter.

Note: Keyboard steering is for testing purposes, we might decide to remove it again.
