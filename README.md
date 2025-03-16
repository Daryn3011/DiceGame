# GeneralizedNontransitiveDiceGame
This repository contains a C# implementation of a dice game that demonstrates fair random selection and intransitive dice relationships. The game is designed to be transparent and provably fair, using cryptographic techniques to ensure that neither the user nor the computer can cheat.

# General Non-Transitive Dice Game

## Key Features

### 🎲 Fair Random Number Generation
- Uses **cryptographically secure random numbers** and **HMAC (SHA-256)** to ensure fairness.
- The user can verify the fairness of the random number generation using the provided HMAC and secret key.

### 🎲 Intransitive Dice Mechanics
- The game features **intransitive dice**, where relationships are not transitive (e.g., Die A beats Die B, Die B beats Die C, but Die C beats Die A).
- Players select dice and roll them to determine the winner.

### 🎮 Interactive Gameplay
- The user and computer take turns selecting dice and rolling them.
- The first move is determined through a **fair random selection process**.

### 📊 Probability Table
- Displays the probability of each dice pair winning against each other.
- Helps users make **informed decisions** when selecting dice.

### ⚠️ Error Handling
- Provides **clear and user-friendly error messages** for invalid inputs or configurations.

---

## 🚀 How It Works

### 1️⃣ First Move Determination
- The computer generates a random number (0 or 1) and its HMAC.
- The user guesses the number.
- If the user guesses correctly, they **choose first**; otherwise, the computer chooses first.

### 2️⃣ Dice Selection
- The **first player** selects a die from the available dice.
- The **second player** selects a die from the remaining dice.

### 3️⃣ Rolling the Dice
- Both players roll their selected dice using the **fair random generation mechanism**.
- The results are displayed with HMAC and secret keys for verification.

### 4️⃣ Determining the Winner
- The player with the **higher roll** wins.
- The results are announced, and the game ends.

---

## 🛠 Getting Started

### Prerequisites
- **.NET SDK** (version 6.0 or higher)

### 🔧 Running the Game
#### 1️⃣ Clone the repository:
```bash
git clone https://github.com/Daryn3011/GeneralizedNontransitiveDiceGame.git
cd GeneralizedNontransitiveDiceGame
```

#### 2️⃣ Run the game with dice configurations as command-line arguments:
```bash
dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7
```

### 🎲 Example Output: 
```bash
I selected a random value in the range 0..1 (HMAC=abc123).
Try to guess my selection.
0 - 0
1 - 1
X - exit
? - help
Your selection: 1
My selection: 1 (KEY=1234567890abcdef).
You guessed correctly! You get to choose first.
Choose your dice:
0 - 2,2,4,4,9,9
1 - 1,1,6,6,8,8
2 - 3,3,5,5,7,7
Your selection: 0
You choose the [2,2,4,4,9,9] dice.
I choose the [1,1,6,6,8,8] dice.
It's time for your roll.
I selected a random value in the range 0..5 (HMAC=def456).
Add your number modulo 6.
0 - 0
1 - 1
2 - 2
3 - 3
4 - 4
5 - 5
X - exit
? - help
Your selection: 2
My number is 3 (KEY=abcdef1234567890).
The fair number generation result is 3 + 2 = 5 (mod 6).
Your roll result is 9.
It's time for my roll.
I selected a random value in the range 0..5 (HMAC=ghi789).
Add your number modulo 6.
0 - 0
1 - 1
2 - 2
3 - 3
4 - 4
5 - 5
X - exit
? - help
```

#### 🛠 Error Handling
#### ❌ No Dice Provided:
```bash
Your selection: 4
My number is 1 (KEY=1234567890abcdef).
The fair number generation result is 1 + 4 = 5 (mod 6).
My roll result is 8.
Your roll result is 9. You win (9 > 8)!
```

#### ❌ Invalid Dice Configuration:
```bash
Error: Invalid dice configuration: 2,2,four,4,9,9. Faces must be integers.
Example usage: dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3
```

#### 📊 Probability Table
When the user selects the help option (?), the game displays a probability table:
```bash
Probability of the win for the user:
+-------------+-------------+-------------+-------------+
| User dice v | 2,2,4,4,9,9 | 1,1,6,6,8,8 | 3,3,5,5,7,7 |
+-------------+-------------+-------------+-------------+
| 2,2,4,4,9,9 | - (0.3333)  | 0.5556      | 0.4444      |
+-------------+-------------+-------------+-------------+
| 1,1,6,6,8,8 | 0.4444      | - (0.3333)  | 0.5556      |
+-------------+-------------+-------------+-------------+
| 3,3,5,5,7,7 | 0.5556      | 0.4444      | - (0.3333)  |
+-------------+-------------+-------------+-------------+
```

