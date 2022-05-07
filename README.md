# PassGen
PassGen is a simple library developed by Marco Santoni under MIT license with the purpose to generate safe random passwords.

## Usage

Create a new console project open your preferred shell and type as follow.

```shell
dotnet new console -o Demo
```

Add reference to `PassGenLib` project and remove all lines from `Program.cs` file then copy the following code to generate a password with default settings.

```csharp
using Santoni1981.PassGenLib;

namespace Demo;

class Program
{
    static void Main()
    {
        Password password = new();
        
        // Generate new password.
        password.NewPassword();
        
        Console.WriteLine(password.PlainText);
    }
}
```
