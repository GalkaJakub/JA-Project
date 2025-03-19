# 🖼️ Image Sharpening – C++ and ASM

This project is a comparative image sharpening application implemented in both C++ and Assembly (ASM).  
It includes a user-friendly GUI built with **C# and WinForms**, allowing users to load image files and apply a sharpening filter with ease. The application supports both single-threaded and multithreaded (up to 64 threads) processing.

---

## Features

- Load and process image files via a WinForms-based interface
- Choose between C++ and ASM versions of the sharpening algorithm
- Switch dynamically between C++ and ASM without restarting the application
- Multi-threaded support for better performance
- Performance analysis (processing time, CPU cycles, etc.)
- Save and display benchmarking results
- Input validation and protection against corrupted files

---

## Technology

- **GUI**: Built using **C# and WinForms**
- **C++**: Implements the sharpening filter using a 3×3 convolution mask  
- **ASM (Assembly)**: Uses vector instructions for efficient parallel pixel processing  
- **Multithreading**: Enables faster computation by distributing work across threads  
- **Edge enhancement**: Highlights image edges by amplifying brightness contrast between neighboring pixels, making the image appear sharper and more detailed

---

## Performance Comparison

The app includes built-in tools to analyze and compare the processing times and CPU usage between C++ and ASM versions.

---

## Author

Jakub Gałka  
[GitHub Profile](https://github.com/GalkaJakub)
