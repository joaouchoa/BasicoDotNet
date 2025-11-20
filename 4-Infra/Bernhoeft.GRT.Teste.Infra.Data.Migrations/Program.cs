using DbUp;
using System.Reflection;


if (args.Length == 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Please provide the connection string as a command-line argument.");
    Console.ResetColor();
    return -1;
}

var connectionString = args[0];

var upgrader = DeployChanges.To
    .PostgresqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .WithVariablesDisabled()
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("FAILURE");
    Console.WriteLine("Script {0} failed executing", result.ErrorScript.Name);
    Console.WriteLine("Error: {0}", result.Error.Message);
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Database migrations successfully applied to PostgreSQL!");
Console.ResetColor();

Console.ReadKey();
return 0;
