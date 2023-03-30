// See https://aka.ms/new-console-template for more information

using PressureApp.Arguments;
using PressureApp.Factories;
using PressureApp.ProfileCreators;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

Console.WriteLine("Hello, World!");

var test = @"
profile:
    type: limitedConcurrency
    arguments: 
       limit: 10

limit: 
    type: queryCount
    arguments: 
        limit: 100

connection: 
    type: Postgres
    connectionString: ${POSTGRES_STRING}

execution: 
    type: query
    arguments: 
        sql: 'SELECT * FROM sys.allobjects'
        
reports: 
    type: csv
    arguments:
        output: file.csv
";

var file = @"
profile:
    type: limitedConcurrency
    arguments: 
       limit: 10
";

// var shell = "querysterss benchmark.yml";

var arguments = Deserialize(file);
Console.WriteLine(arguments);

var factory = new LoadProfilesFactory(new[]
{
    new LimitedConcurrencyLoadProfileCreator()
});

var model = factory.CreateProfile(arguments);

Console.ReadLine();

Arguments Deserialize(string fileContent)
{
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    return deserializer.Deserialize<Arguments>(fileContent);
}