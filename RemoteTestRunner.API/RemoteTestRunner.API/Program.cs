using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("test", () =>
{
    Process cmd = new Process();
    cmd.StartInfo.FileName = "cmd.exe";
    cmd.StartInfo.RedirectStandardInput = true;
    cmd.StartInfo.RedirectStandardOutput = true;
    cmd.StartInfo.CreateNoWindow = true;
    cmd.StartInfo.UseShellExecute = false;
    cmd.StartInfo.WorkingDirectory = @"D:\code\RemoteTestRunner.API\RemoteTestRunner.RunningTests";
    cmd.Start();
    
    cmd.StandardInput.WriteLine("dotnet build");
    cmd.StandardInput.WriteLine("dotnet test");

    cmd.StandardInput.Flush();

    cmd.StandardInput.Close();
    cmd.WaitForExit();
    var r = cmd.StandardOutput.ReadToEnd();

    return r;
});

app.MapGet("template", () =>
{
    var lines = File.ReadAllLines("Templates/SimpleMath.Pow.Template.txt");

    return lines;
});

app.MapPost("template", ([FromBody]string requestCode) =>
{
    var lines = File.ReadAllLines("Templates/SimpleMath.Pow.Template.txt").ToList();

    var sb = new StringBuilder();

    foreach (var line in lines)
    {
        if (line.Contains("<insertCodeHere>"))
        {
            var lineIndex = lines.IndexOf(line);

            sb.Append(requestCode);
        }
        else
        {
            sb.AppendLine(line);
        }
    }

    var result = sb.ToString();

    File.WriteAllText(@"D:\code\RemoteTestRunner.API\RemoteTestRunner.Subject\SimpleMath.cs", result);

    return result;
});

app.Run();