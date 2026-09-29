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

### UI Event Handlers

UI actions such as button clicks use an event handler with a descriptive name.

For example:

```csharp
private void WaterRose_Click(object sender, RoutedEventArgs e)
{
    rosePetals += 1;
    UpdateUI();
}
```

The event handler should handle the action caused by the user. UI elements use descriptive names and their event handlers clearly describe what happens when the user interacts with them.

### Calculations

Game calculations that follow a specific formula should be placed in a separate method when possible.

For example, the Watering Can price is calculated in its own method:

```csharp
private double GetWateringCanCost()
{
    return wateringCanBaseCost *
           Math.Pow(wateringCanCostMultiplier, wateringCansPurchased);
}
```

Keeping calculations in a separate method makes the code easier to read and allows the calculation to be reused without duplicating the formula.

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

The current implementation covers **User Stories 1 to 7**.

### User Story 1: Basic Income

> **As a player, I want a basic income per second so I earn currency from the start.**

The player automatically earns **Rose Petals** over time.

#### Current variables

```csharp
private double rosePetals = 0;
private double petalsPerSecond = 1;
```

* `rosePetals` stores the player's current amount of Rose Petals.
* `petalsPerSecond` stores the amount of Rose Petals earned per second.

#### Game Timer

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

#### User Interface

The HUD displays:

* The current amount of **Rose Petals**.
* The current **Petals per Second**.

---

### User Story 2: Manual Income

> **As a player, I want to increase income through manual actions, so I can earn currency faster.**

The player can manually earn Rose Petals by watering the rose.

```csharp
private void WaterRose_Click(object sender, RoutedEventArgs e)
{
    rosePetals += 1;
    UpdateUI();
}
```

The interface provides a clear instruction and button for the manual action.

---

### User Story 3: Upgrades

> **As a player, I want to buy upgrades that increase my income per second, so I can grow faster.**

The player can purchase upgrades that increase **Petals per Second**.

Each upgrade displays:

* Price
* Effect
* Requirement

Upgrades that the player cannot afford are disabled.

After purchasing an upgrade, its effect is applied immediately.

---

### User Story 4: Automation

> **As a player, I want to unlock automation that replaces manual actions, so the game runs without interaction.**

The player can unlock the **Bee Keeper** automation.

The Bee Keeper uses its own `DispatcherTimer` and produces Rose Petals independently from the main game timer.

Production is tracked and displayed in the production log.

The produced Rose Petals are added to the player's total amount.

---

### User Story 5: Scalable Shop Costs

> **As a player, I want scalable costs, so upgrades remain scalable as I progress.**

Upgrade prices increase based on the number of times an upgrade has been purchased.

The price follows this formula:

```text
base price × multiplier ^ number purchased
```

For example, the Watering Can starts at 10 Rose Petals and increases after each purchase.

The calculation is placed in a separate method:

```csharp
private double GetWateringCanCost()
{
    return wateringCanBaseCost *
           Math.Pow(wateringCanCostMultiplier, wateringCansPurchased);
}
```

Keeping the calculation in a separate method makes the formula reusable and avoids duplicating the calculation.

---

### User Story 6: Save Progress

> **As a player, I want to save my progress so I can continue later.**

The game can save the current progress to a local JSON file.

A separate `GameSaveData` class is used to define the data that needs to be saved.

The save data contains:

* Current Rose Petals
* Petals per second
* Number of Watering Cans purchased
* Bee Keeper unlocked status
* Number of Bee Keeper productions
* Save timestamp

The data is converted to JSON using `System.Text.Json`.

The game provides a manual **Save Game** button that calls the `SaveGame()` method.

The save file is stored in the user's local application data folder:

```text
AppData\Local\RosaLunaris\savegame.json
```

The directory is created automatically if it does not exist.

#### Autosave

The game also uses a separate `DispatcherTimer` for autosaving.

The autosave timer runs every 30 seconds:

```csharp
autosaveTimer.Interval = TimeSpan.FromSeconds(30);
```

When the timer ticks, it calls the same `SaveGame()` method used by the manual save button:

```csharp
private void AutosaveTimer_Tick(object? sender, EventArgs e)
{
    SaveGame();
}
```

This prevents duplicate save logic and ensures that manual saving and autosaving use the same process.

---

### User Story 7: Load Progress

> **As a player, I want to load my progress so I can resume an earlier session.**

The game automatically attempts to load the saved progress when the application starts.

If a save file exists, the JSON data is deserialized and the saved game values are restored.

The loaded values include:

* Current Rose Petals
* Petals per second
* Number of Watering Cans purchased
* Bee Keeper unlocked status
* Number of Bee Keeper productions

If the Bee Keeper was unlocked when the game was saved, its timer is started again after loading.

If no save file exists, the game starts with its normal default values.

#### Corrupt Save Handling

The save file is loaded inside a `try/catch` block.

If the JSON file cannot be loaded or contains invalid data:

1. An error message is shown to the player.
2. The invalid save data is not used.
3. The game starts with safe default values.

The default values are:

```text
Rose Petals: 0
Petals per second: 1
Watering Cans purchased: 0
Bee Keeper: locked
Bee Keeper productions: 0
```

This prevents the application from crashing when the save file is corrupt.
