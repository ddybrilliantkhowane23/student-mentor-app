// MNQOBI KHOWANE 41892712
// Student Mentor Engine - C++ Backend
// This program provides therapeutic advice for students and saves it to a file
// File: StudentMentor.cpp

#include <iostream>
#include <fstream>
#include <string>

using namespace std;

// Function to display available advice categories to the user
void displayCategories() {
    cout << "\n========= Student Mentor System =========\n";
    cout << "Select a category of advice:\n";
    cout << "1. Study Tips\n";
    cout << "2. Coding Guidance\n";
    cout << "3. Social & Relationships\n";
    cout << "4. Motivation & Mental Health\n";
    cout << "5. Career & Growth\n";
    cout << "6. Exit Program\n";
    cout << "=========================================\n";
}


// Function to display advice and save it to file for C# to read
// Parameters: tips array, array size, student name, category name
void displayAllAdvice(const string tips[], int size, const string& name, const string& categoryName) {
    cout << "\nHey " << name << "! Here are some " << categoryName << " for you:\n";
    for (int i = 0; i < size; ++i) {
        cout << "- " << tips[i] << endl;
    }


// ===== FILE I/O: Saving data for C# application =====
// Save advice to USB drive in format that C# frontend expects
// This creates the communication bridge between C++ and C#
    ofstream file("E:/advices.txt");
    if (file.is_open()) {
        // Save category name in brackets so C# can identify sections
        file << "[" << categoryName << "]\n";

        // Save each tip (without bullet points for clean C# reading)
        for (int i = 0; i < size; ++i) {
            file << tips[i] << endl;
        }
        file.close();
        cout << "\nAdvice successfully saved to USB Drive (E:/advices.txt)\n";
        cout << "This file can now be read by the C# Student Mentor App!\n";
    } else {
        cout << "Error: Unable to save advice to file.\n";
        cout << "Please check if USB drive is connected as E: drive\n";
    }

    cout << "\nStay strong, " << name << " , you have got this!\n";
}

int main() {
    string name;
    int category;

    // Get student name for personalized experience
    cout << "Enter your first name: ";
    getline(cin, name);

    // Main program loop - runs until user chooses to exit
    do {
        displayCategories();
        cout << "Choose a category (1-6): ";

        // ===== INPUT VALIDATION: Prevent program crashes =====
        // Check if input is actually a number
        if (!(cin >> category)) {
            cout << "\nInvalid input. Please enter a number between 1 and 6.\n";
            cin.clear(); // Clear error flag from cin
            cin.ignore(1000, '\n');  // Clear input buffer to prevent infinite loops
            continue; // Restart the loop
        }

        // Clear input buffer after reading number (important for getline later)
        cin.ignore(1000, '\n');

        if (category == 6) {
            cout << "\nExiting program... Goodbye " << name << "!\n";
            break;
        }

        // ===== ADVICE DATABASE: All therapeutic tips stored in arrays =====
        // Study Tips Array
        string studyTips[] = {
            "Study smart, not hard - plan your sessions around topics you struggle with.",
            "Use the Pomodoro method: 25 minutes focus, 5 minutes rest.",
            "Summarize every chapter in your own words - it helps memory stick.",
            "Group studies are powerful.Teach your peers what you have learned.",
            "Avoid cramming. Consistency beats last-minute panic every time."
        };

        // Coding Advice Array
        string codingAdvice[] = {
            "Code daily. Even 15 minutes a day builds coding muscle.",
            "Debug with patience. Errors are lessons in disguise.",
            "Read others codes. You will learn tricks no lecture can teach.",
            "Build small projects. They teach more than big tutorials.",
            "Google and Stack Overflow are not cheating. They are part of the craft."
        };

        // Social Tips Array
        string socialTips[] = {
            "Make friends with seniors. They have walked your path before.",
            "Do not isolate yourself. Your classmates are your first network.",
            "Be kind to everyone. Todays classmate might be tomorrows boss.",
            "Ask questions in class . Curiosity attracts good company.",
            "Balance your social life with studies. Burnout is real."
        };

        // Motivation Tips Array
        string motivationTips[] = {
            "You belong here . you have already made it this far.",
            "Failure is not the opposite of success. It is the training ground.",
            "Keep pushing. Progress feels invisible until it is obvious.",
            "Take breaks when you need to. Rest is part of the process.",
            "Your dreams are valid. Chase them one line of code at a time."
        };

        // Career Tips Array
        string careerTips[] = {
            "Build your LinkedIn early. Your digital footprint matters.",
            "Connect with developers. Ask questions, learn from them.",
            "Start small: contribute to GitHub, it shows initiative.",
            "Internships teach you faster than any lecture.",
            "Keep learning new languages as you know tech changes, stay adaptable."
        };

        // ===== CATEGORY ROUTING: Direct to appropriate advice =====
        switch (category) {
            case 1:
                displayAllAdvice(studyTips, 5, name, "Study Tips");
                break;
            case 2:
                displayAllAdvice(codingAdvice, 5, name, "Coding Guidance");
                break;
            case 3:
                displayAllAdvice(socialTips, 5, name, "Social & Relationships");
                break;
            case 4:
                displayAllAdvice(motivationTips, 5, name, "Motivation & Mental Health");
                break;
            case 5:
                displayAllAdvice(careerTips, 5, name, "Career & Growth");
                break;
            default:
                cout << "\nInvalid category selected.\n";
                break;
        }

        // Pause before showing menu again
        cout << "\nPress Enter to continue...";
        string dummy;
        getline(cin, dummy);

    } while (true); // Infinite loop - exits when user chooses option 6

    return 0;
}
