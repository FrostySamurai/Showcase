![GitHub](https://img.shields.io/github/license/FrostySamurai/Showcase)

# Showcase
Simple UI system for Unity.

## Screens
Simple system for screen management. Allows showing and hiding of various ui elements. These can be split into layers with unique identifiers that can be split between multiple canvases.

Each screen has associated parameters type, which are in general used to interact with ScreenManager.

### Screen Manager
Class through which the entire screen system can be accessed.

### Screen Initialization & Lifecycle

When overriding relevant lifecycle functions it is important to still call base implementation.
```csharp
public override void Init(SomeDialogueParameters parameters)
{
	base.Init(parameters);

	// do stuff
}

public override void Show()
{
	base.Show();

	// do stuff
}

public override void Hide()
{
	base.Hide();

	// do stuff
}
```

#### Example Usage
```csharp
_screenManager.Show(new SomeDialogueParameters("some data"));
_screenManager.Hide<SomeDialogueParameters>();
```

### Dialogue Screen
A part of UI (for example a popup window) where only one of these elements can be visible in any given layer. If a dialogue is shown while other dialogue in the same layer is active, the other dialogue will be hidden.

### Panel Screen
A part of UI where any number of these can be visible at any given time, regardless of layer they are within.

### Layer Registration
While basic layer registration is implemented, it requires direct reference to ScreenManager. This is not ideal, or outright unusable when using multiple scenes and should serve only as an example.

It is recommended to implement custom layer registration based on used architecture, such as having a singleton with reference to ScreenManager, using dependency injection framework etc.