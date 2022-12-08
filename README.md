# GameOfLife
This project is implementation on [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)

---
This Core Class is wraped around C# ASP.NET Minimal API framework to provide web endpoint. Ducumentation of a API in Swager is [here](Azure)

Thie endpoint is used by client side JavaScript aplication to visualise bahavior of a cells.

Libraries used in Project:
- .NET 
- ASP.NET
- FluentValidation
- ScottPlot
- SixLabors.ImageSharp
---

## Rules of the Game:
- Any live cell with fewer than two live neighbours dies (referred to as underpopulation).
- Any live cell with more than three live neighbours dies (referred to as overpopulation).
- Any live cell with two or three live neighbours lives, unchanged, to the next generation.
- Any dead cell with exactly three live neighbours comes to life

---

### Steps:
- Step 1
![Step1](.\Images\outputP1.png)
- Step 2
![Step2](.\Images\outputP2.png)
- Step 3
![Step3](.\Images\outputP3.png)
- Step 4
![Step4](.\Images\outputP4.png)
- Step 5
![Step5](.\Images\outputP5.png)

---

