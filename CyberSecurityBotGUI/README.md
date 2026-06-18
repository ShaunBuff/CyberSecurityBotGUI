# 🔐 Cybersecurity Awareness Chatbot

## 👤 Student Information

- **Name:** Shaun Julian Buffel  
- **Student Number:** ST10465890  
- **Module:** Programming 2A (PROG6221)  
- **Assignment:** POE Part 2 – GUI Cybersecurity Chatbot  

---

## 📌 Project Overview

This project is a **Cybersecurity Awareness Chatbot** developed using **C#, WPF, and .NET 8.0** in Visual Studio 2022.

It is an extension of the Part 1 command-line chatbot and has been transformed into a full **Graphical User Interface (GUI) application**.

The chatbot educates users about cybersecurity topics such as:
- Phishing attacks
- Password safety
- Malware protection
- Online scams
- Privacy awareness

It provides interactive responses, sentiment detection, memory recall, and dynamic conversation flow.

---

## 🎯 Project Objectives

The main goal of this project is to:
- Transform a console chatbot into a WPF GUI application
- Implement cybersecurity awareness education features
- Apply object-oriented programming principles
- Use collections (dictionaries and lists)
- Implement delegates
- Create a user-friendly interactive chatbot

---

## 🖥️ Features Implemented

### 🎨 GUI Features
- WPF graphical interface
- Chat display window
- Input textbox and send button
- ASCII art header display
- Clean and user-friendly layout

---

### 🔊 Multimedia Features
- Voice greeting (WAV file)
- Automatically plays when application starts

---

### 🧠 Chatbot Intelligence
- Keyword recognition (phishing, password, malware, privacy, scam)
- Random responses using collections
- Sentiment detection (worried, confused, frustrated)
- Memory and recall (user name and favourite topic)
- Conversation flow handling (“tell me more”, “another tip”)

---

### ⚙️ Programming Concepts Used
- Object-Oriented Programming (OOP)
- Delegates (ResponseHandler)
- Dictionaries and Lists (Collections)
- Event-driven programming
- Input validation and error handling

---

## 🧾 Code Structure

- `MainWindow.xaml` → GUI layout design
- `MainWindow.xaml.cs` → User interaction logic
- `Chatbot.cs` → Core chatbot logic (keywords, responses, sentiment, delegate)

---

## 📊 Part 2 Rubric Alignment

| Requirement | Implementation | Evidence |
|-------------|----------------|----------|
| GUI Design (WPF) | Full WPF interface with chat system | MainWindow.xaml |
| Voice Greeting | WAV file plays on startup | MainWindow.xaml.cs |
| ASCII Art | Displayed in GUI header | MainWindow.xaml |
| Keyword Recognition | Dictionary-based detection | Chatbot.cs |
| Random Responses | List + Random selection | Chatbot.cs |
| Conversation Flow | Follow-up handling ("tell me more") | MainWindow.xaml.cs |
| Memory & Recall | Stores name and favourite topic | MainWindow.xaml.cs |
| Sentiment Detection | Detects emotions and adjusts response | Chatbot.cs |
| Collections Usage | Dictionary<string, List<string>> | Chatbot.cs |
| Delegate Usage | ResponseHandler delegate implemented | Chatbot.cs |
| Error Handling | Default fallback response | Chatbot.cs |
| Input Validation | Prevents empty inputs | MainWindow.xaml.cs |
| Multimedia Integration | WAV voice greeting included | Project files |

---

## 🚀 How to Run the Project

1. Clone the repository:
```bash
git clone https://github.com/ShaunBuff/CyberSecurityBot