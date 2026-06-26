# Cybersecurity Awareness Chatbot

## Student Information

* Name: Shaun Julian Buffel
* Student Number: ST10465890  

---

## 📌 Project Overview

This project is a GUI-based Cybersecurity Awareness Chatbot developed using **C# (WPF)** and **MySQL**.

The system educates users about cybersecurity concepts while providing interactive features such as a chatbot, task manager, quiz game, NLP simulation, and activity logging system.

---

## 🚀 Features

### 💬 Chatbot System

* Interactive cybersecurity chatbot
* Natural language responses
* Keyword-based NLP simulation
* Handles user greetings and queries

---

### 🗂 Task Assistant (MySQL Integration)

* Add cybersecurity-related tasks
* Store tasks in MySQL database
* View and delete tasks
* Optional reminder system (date-based)

Example:

* "Add task enable firewall"
* "Set reminder for 3 days"

---

### 🎮 Cybersecurity Quiz Game

* 10+ multiple-choice questions
* One question displayed at a time
* Immediate feedback (Correct / Incorrect)
* Final score displayed at the end
* Cybersecurity education reinforcement

---

### 🧠 NLP Simulation

* Detects user intent using keyword matching
* Recognises variations of input such as:

  * "add task"
  * "remind me"
  * "phishing"
  * "password"
* Reduces need for exact phrasing

---

### 📊 Activity Log Feature

* Records all chatbot actions
* Logs:

  * Task creation
  * Reminder setup
  * Quiz activity
  * User interactions
* Displays last 5–10 actions

---

## 🛠 Technologies Used

* C#
* WPF (XAML)
* MySQL Database
* Visual Studio
* Object-Oriented Programming
* Event-driven programming

---

## 🗄 Database Setup

1. Create database:

```sql
CREATE DATABASE cybersecuritybotdb;
```

2. Create tasks table:

```sql
CREATE TABLE tasks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255),
    description TEXT,
    reminder DATETIME NULL
);
```

---

## ▶ How to Run the Project

1. Clone repository
2. Open solution in Visual Studio
3. Ensure MySQL Server is running
4. Update database connection string if needed
5. Build and run project (F5)

---

## 🎥 Video Presentation

https://www.youtube.com/watch?v=HTbqqblK5k8

---

## 📌 Notes

This project demonstrates:

* GUI development using WPF
* Database integration using MySQL
* Basic NLP simulation using string manipulation
* Interactive learning through gamification (quiz)
* Activity tracking system

---

## ✅ Submission Checklist

* [x] Chatbot implemented
* [x] Task system working
* [x] Quiz functional
* [x] NLP simulation included
* [x] Activity log working
* [x] MySQL integration complete
* [x] GitHub repository structured
* [x] Video uploaded (Unlisted YouTube)
