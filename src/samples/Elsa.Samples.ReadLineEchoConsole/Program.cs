﻿using System.Threading.Tasks;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Samples.ReadLineEchoConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa()
                .AddConsoleActivities()
                .BuildServiceProvider();
            
            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();
            
            // Build a new workflow.
            var workflow = services.GetService<IWorkflowBuilder>()
                .WriteLine("What's your name?")
                .ReadLine()
                .WriteLine(context => $"Greetings, {context.Input}!")
                .Build();
            
            // Get the workflow host.
            var workflowHost = services.GetService<IWorkflowHost>();

            // Execute the workflow.
            await workflowHost.RunWorkflowAsync(workflow);
        }
    }
}