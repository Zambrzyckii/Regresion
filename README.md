# Advanced Multivariable & Polynomial Regression Engine
This Project is a custom-built, high-performance compute engine implementing linear and polynomial regression algorithms using the Normal Equation. The system architecture is desgined around the Extract, Transform, Load paradigm and the Separation of Concerns principle, combining the strengths of the .NET ecosystem as the compute layer and Python (Pandas) as a flexible microservice for data preprocessing.
The entire solution is fully containerized using Docker, eliminating Deployment Hell and ensuring immediate execution of the analytical pipeline in any environment

## Architecture
Project deliberately forgoes out-of-the-box ML frameworks (like ML.NET or Scikit-Learn) to demonstrate a deep understanding of the mathematical foundation of machine learning and software architecture

### 1. Layer Separation
* **Preprocessing Layer:** Python and Pandas, responsible for data cleaning, automatic detection of cetegorical types, and transforming text into binary vector - **One-Hot Encoding**. This makes the engine resilient to parsing errors and dynamically adjusts the width of the input matrix
* **Compute Layer:** C#, focused exclusively on pure, optimized linear algebra. It accepts a perfectly prepared matrix of `double` structures, builds a Design Matrix, and solves the system of equations.

### 2. Native algorithms implementations
Within the C# analytical module, the following have been implemented from scratch without external math libraries:
* Matrix multiplication for arbitrary dimensions (optimized $O(n^3)$ loops)
* Matrix transposition
* Matrix inversion using the Gauss-Jordan elimination method
* Polynomial evaluation and Mean Squared Error (**MSE**) calculation
### 3. Hybrid Containerization 
The Docker image utilizes an advanced multi-stage build process. The container's OS  installs both the .NET Core runtime and native Python packages including Pandas. This ensures complete isolation and removes the need to install any dependencies on the host machine

---

##  Project Structure

```text
Regresion/
├── Regresion/                 # Main C# console application 
│   ├── Algorithms/            # Mathematical implementation of linear algebra
│   ├── Loader/                # Data loading layer 
│   ├── Outputs/               # Result loading layer 
│   ├── Inputs/                # Data model  
│   └── Program.cs             # Pipeline orchestration, CLI, and chart generation
├── Encoding/                  # Data preprocessing script 
│   └── encode.py              # One-Hot Encoding mechan
├── Algorithms.Tests/          # Comprehensive suite of unit tests (xUnit)
├── Dockerfile                 # Multi-runtime environment 
├── gold_futures_timeseries.csv # CSV file on which the project was based
└── Regresion.sln              # .NET Solution file
```
## How to run
The application communicates with the outside world through secure Docker Volumes, allowing for dynamic passing of CSV files from the host system and safely retrieving generated graphical reports
1. Install Docker
2. Clone repo ``` git clone https://gitlab.com/ZambrzyckiBartosz/regresion.git ```
3. Enter project directory
4. Run following command ``` docker build -t regression-app . ```
5. Run full analytical pipline 
**Example run for the gold_futures_timeseries.csv file analyzing price dependency on moving averages ma_30 and ma_90**
``` bash 
docker run -it --rm -v "$(pwd):/data" regression-app -p /data/gold_futures_timeseries.csv -x ma_30 ma_90 -y close 
```
6. Check results
* Check CLI output featuring Calculated wieght coefficients, totaln mean squared error, a tabular summary of excepted values vs predictions
* Check Actual vs Predicted Chart saved in samples.png. This is a high-resolution scatter plot generated using ScottPlot engine, showcasing the regression model's goodness of fit to the real market data