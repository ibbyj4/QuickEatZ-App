# QuickEatZ – Mobile Recipe App (.NET MAUI, C#)

QuickEatZ is a mobile recipe app demonstrating authentication, text-to-speech, geolocation, and recipe management. Built with **.NET MAUI (C#)** to showcase cross-platform mobile development and use of device features.

## ✨ Features
- **User Login & Authentication** (username/password)
- **Recipe CRUD**: add, search, view recipes
- **Text-to-Speech** welcome (“Welcome to QuickEatZ — let’s get cooking!”)
- **Geolocation**: fetch current coordinates (foundation for location features)
- **Responsive MAUI UI** with simple, clean navigation

## 🧰 Tech Stack
- **Languages:** C#, XAML
- **Framework:** .NET MAUI
- **IDE:** Visual Studio 2022
- **APIs/Libs:** MAUI Essentials (TTS, Geolocation)

## ▶️ Getting Started
**Prereqs**
- Visual Studio 2022 with **.NET Multi-platform App UI (MAUI)** workload
- Android SDK / Emulator (or physical device)

**Run**
1. Clone: `git clone https://github.com/<your-username>/QuickEatZ-Recipe-App.git`
2. Open the `.sln` in Visual Studio
3. Restore NuGet packages
4. Select **Android Emulator** (or device) and **Run**

## 🧪 Testing & Notes
- Manual test cases for Login, Add/Search Recipe, TTS, and Geolocation
- Known limitations: geolocation shown as raw coords (future: nearby stores/filters)
- Future ideas: categories, favorites, image upload, share recipes

## 📚 Learning Goals
Demonstrates use of mobile hardware APIs, auth, CRUD patterns, and MAUI UI patterns in a compact project.

## 📝 License
MIT
