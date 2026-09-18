# Code Conventions - Rosa Lunaris

This document describes the coding conventions used in the **Rosa Lunaris** project.

## 1. Naming Conventions

### Variables

Variables use **camelCase**.

Examples:

```csharp
double rosePetals;
double petalsPerSecond;
```

Variable names should clearly describe what the variable contains.

### Methods

Methods use **PascalCase**.

Examples:

```csharp
void UpdateUI()
void GameTimer_Tick()
```

Method names should clearly describe what the method does.

### XAML Elements

XAML elements that need to be accessed from C# use a descriptive `x:Name`.

Examples:

```xml
<TextBlock x:Name="RosePetalsText" />
<TextBlock x:Name="PetalsPerSecondText" />
```

The name should clearly describe the purpose of the element.

---

## 2. Methods

Methods should have **one clear responsibility**.

For example:

* `UpdateUI()` is responsible for updating the information displayed to the player.
* `GameTimer_Tick()` handles the income update when the game timer ticks.

Keeping responsibilities separated makes the code easier to understand and maintain.

---

## 3. Code Formatting

Code should be kept readable and consistently formatted.

The following rules are used:

* Use consistent indentation.
* Use clear spacing between logical sections.
* Use descriptive names.
* Avoid unnecessary code.
* Keep related code together.

---

## 4. Comments

Comments should only be used when they provide useful additional information.

Comments should preferably explain **why** something is done rather than explaining code that is already obvious.

---

## 5. User Interface

The user interface should use clear and descriptive labels.

For example:

```text
🌹 Rose Petals
+1.0 Petals per second
```

The interface follows the **Rosa Lunaris** theme, based around:

* 🌹 Roses
* 🌙 The moon
* 🌿 Nature
* ✨ A magical garden

Decorative elements should not make important information difficult to understand.

---

## 6. Git

Git is used to keep track of changes made during development.

Commits should:

* Describe one logical change.
* Use a short and clear message.
* Be made regularly.

Example:

```text
Add basic income system
```

---

## 7. Current Implementation

The current implementation covers **User Story 1: Basic Income**.

> **As a player, I want a basic income per second so I earn currency from the start.**

The player automatically earns **Rose Petals** over time.

### Current variables

```csharp
private double rosePetals = 0;
private double petalsPerSecond = 1;
```

* `rosePetals` stores the player's current amount of Rose Petals.
* `petalsPerSecond` stores the amount of Rose Petals earned per second.

### Game Timer

A `DispatcherTimer` is used to update the game:

```csharp
gameTimer.Interval = TimeSpan.FromMilliseconds(100);
```

The timer runs every **100 milliseconds**, which results in **10 updates per second**.

The income is divided over these updates:

```text
1 Petal per second
÷ 10 updates
= 0.1 Petal per update
```

This allows the Rose Petals counter to update continuously.

### User Interface

The HUD currently displays:

* The current amount of **Rose Petals**.
* The current **Petals per Second**.

This satisfies the requirements for the first User Story.
