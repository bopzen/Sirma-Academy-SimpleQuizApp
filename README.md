# SimpleQuizApp
## This is an exam project for Sirma Academy ASP.NET MVC Course

## Project Description:
- A web-based app **General Knowledge Quiz** built with **ASP.NET MVC**.
- Choose from three difficulty levels — **Easy**, **Medium**, and **Hard** — each containing **10 random questions** based on the selected difficulty.
- After completing the quiz a **detailed results page** is shown listing all questions with your answers and whether they were correct or not.

## Tech Description:
- As per requirement no DB is used, the questions data is read from a **JSON** file
- The **JSON** file consists of **20 questions** per difficulty. Every time the quiz is started it takes **10 random** questions and also **shuffles** the answers.
- The app uses typical **ASP.NET MVC** architecture with additional **Data layer** to access the data from the file and additional **Service** layer to operate with the data, state and time
- As per requirement **Static class with static list and dictionary** were used to keep track of the answered questions between each question form submission (**initially I used sessions to keep the state as it close to real life app but after second reading of the requirements I noticed that it is not recommended. There is a branch that uses sessions:** https://github.com/bopzen/Sirma-Academy-SimpleQuizApp/tree/UseSessionsForStateKeeping)
**BONUS:**
- Simple custom **CSS** and **Bootstrap** used to style the app
- Added dynamic **timer** for the quiz with total limit of **10 mins**

## How to run the app locally:
#### 1. Clone the Repository
`git clone https://github.com/bopzen/Sirma-Academy-SimpleQuizApp.git`
#### 2. Navigate into the Project Directory
`..\SimpleQuizApp\`
#### 3. Open the Project
`Open the solution file (SimpleQuizApp.sln) in Visual Studio`
#### 4. Build and Run
`Press F5 or click Start Debugging to build and launch the app.`

## Tech Stack: 
- **ASP.NET Core MVC / .NET 8**
- **C#**
- **Razor Views**
- **JSON File/In-Memory Data**
- **Bootstrap / Custom CSS**
- **Vanilla JS**

## Simple Folder Structure:
```
SimpleQuizApp/
├── Controllers/
│   ├── HomeController.cs
│   └── QuizController.cs
├── Data
│ 	├── Models/
│	│	├── Option.cs
│	│	└── QuizQuestion.cs
│	├── QuizRepository.cs
│	└── general_knowledge_quiz_questions.json
├── Models/
│	├── ErrorViewModel.cs
│	├── QuestionViewModel.cs
│	├── QuizResultItem.cs
│   └── QuizResultViewModel.cs
├── Services/
│	├── QuizService.cs
│	├── QuizState.cs
│	└── QuizTime.cs
├── Views/
│   ├── Home/
│	│	└── Index.cshtml
│   └── Quiz/
│		├── Question.cshtml
│		└── Result.cshtml
├── wwwroot/
│   ├── css/
│   └── js/
└── SimpleQuizApp.sln
```

## Preview:
#### Home Page:

<img width="1912" height="907" alt="Screenshot 2025-10-28 234743" src="https://github.com/user-attachments/assets/5ae8b169-4f22-4311-804b-3966d0ec9689" />

#### Questions Page:

<img width="1913" height="905" alt="Screenshot 2025-10-28 234804" src="https://github.com/user-attachments/assets/1006b2d5-8b3e-41fd-85f1-87521d5124a7" />

#### Results Page:

<img width="1919" height="911" alt="Screenshot 2025-10-28 235023" src="https://github.com/user-attachments/assets/714dd2a4-e2c8-4c32-8f88-0ede5a3ad8c5" />
