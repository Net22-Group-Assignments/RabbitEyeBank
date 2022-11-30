﻿using RabbitEyeBankConsole;
using RabbitEyeBankLibrary.Shared;
using Serilog;
using Serilog.Sinks.SpectreConsole;

using var log = new LoggerConfiguration().MinimumLevel
    .Debug()
    .WriteTo.Debug()
    .WriteTo.SpectreConsole()
    .CreateLogger();
Log.Logger = log;
Log.Information("Bank console starting.");
BogusSetup.InitData(5, 5);
var appTask = Application.AppTask();
var transactionTask = Application.TransactionTask();
Task.WaitAll(appTask, transactionTask);
