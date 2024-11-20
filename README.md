# FooFuuApp

## Description

FooFuuApp is a console application that processes a range of numbers based on specific rules and outputs the results to either the console or a file. The application uses dependency injection, logging, and error handling to ensure robust and maintainable code.

## How It Works

1. **Argument Parsing**: The application starts by parsing command-line arguments to determine the range of numbers to process and the output device (console or file).
2. **Input Validation**: The input range is validated to ensure it is within acceptable bounds.
3. **Number Processing**: The application processes each number in the specified range using a custom processor (`FooFuuProcessor`).
4. **Output**: The results are output to the specified device (console or file).

## How to Run the Program

### Prerequisites

- .NET SDK installed on your machine.
- NLog configuration file (`nlog.config`) in the root directory.

### Running the Application

1. **Clone the Repository**:

   ```sh
   git clone https://github.com/milanLavic/FooFuuApp.git
   cd FooFuuApp
   ```

2. **Build the Application**:

   ```sh
   dotnet build
   ```

3. **Run the Application**:

   ```sh
   dotnet run /start <start> /end <end> /outputDevice <outputDevice> /filePath <filePath>
   ```

   - `<start>`: The starting number of the range (default is 1).
   - `<end>`: The ending number of the range (default is 100).
   - `<outputDevice>`: The output device (`console` or `file`). -> default is console
   - `<filePath>`: The file path for output if `outputDevice` is `file`.

### Example

To process numbers from 1 to 100 and output to the console:

```sh
dotnet run /start 1 /end 100 /outputDevice console
```

To process numbers from 1 to 100 and output to a file:

```sh
dotnet run /start 1 /end 100 /outputDevice file /filePath output.txt
```

### Running the Application with Windows Terminal

1. **Navigate to the Build Directory**:

   ```sh
   cd FooFuuApp\bin\Debug\net8.0
   ```

2. **Run the Executable**:
   ```sh
   FooFuuApp.exe /start <start> /end <end> /outputDevice <outputDevice> /filePath <filePath>
   ```

### Example with Executable

To process numbers from 1 to 100 and output to the console:

```sh
FooFuuApp.exe /start 1 /end 100 /outputDevice console
```

To process numbers from 1 to 100 and output to a file:

```sh
FooFuuApp.exe /start 1 /end 100 /outputDevice file /filePath output.txt
```

## Launch Settings

To include the specified launch settings, you need to add the following JSON configuration to your `launchSettings.json` file. This file is typically located in the `Properties` folder of your project.

```json
{
  "profiles": {
    "FooFuuApp": {
      "commandName": "Project",
      "commandLineArgs": "/s 100 /e 200"
    },
    "FooFuu - temp file": {
      "commandName": "Project",
      "commandLineArgs": "/s 100 /e 200 /output file /filepath \"%Temp%/MyOutput.txt\""
    },
    "FooFuu - no input": {
      "commandName": "Project"
    }
  }
}
```

You can find the `MyOutput` file in `%Temp%/MyOutput.txt`.

## Logging

The application uses NLog for logging. Ensure that the `nlog.config` file is correctly configured to capture logs as needed.

## Error Handling

The application includes comprehensive error handling to manage validation errors and unexpected exceptions, ensuring that meaningful messages are logged and displayed to the user.
