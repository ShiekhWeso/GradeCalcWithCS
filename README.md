# GradeCalcWithCS — GPA Calculator (Console Beta)

Welcome to **GradeCalcWithCS**, a C# console application that helps students track their academic performance through GPA and percentage calculations. This is the **beta version**, built entirely in the console, and serves as the foundation for a future GUI release.

---

## Features

-  Add, view, edit, and delete student records
-  Calculate GPA and total percentage based on subject marks and credit hours
-  Assign letter grades using a customizable grading scale
-  Save and load student data using JSON for persistence
-  Sort students by GPA or name
-  Input validation for names, subjects, marks, and credit hours

---

## Technologies Used

- C# (.NET 6+)
- Console UI
- JSON serialization
- Regex for input validation
- Object-oriented design (`Student`, `Subject` classes)

---

## How to Run
```bash
git clone https://github.com/ShiekhWeso/GradeCalcWithCS
cd GradeCalcWithCS
dotnet publish -p:PublishSingleFile=true --self-contained
```
the executable will be in `bin/Release/net9.0/publish/`
