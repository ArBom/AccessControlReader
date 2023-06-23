// See https://aka.ms/new-console-template for more information
using AccessControlReader;
using AccessControlReader.Devices;
using AccessControlReader.StateMachine;

StateMachine stateMachine = new();

Configurator configurator = new();
State.SetTexts(configurator.XElementOfConfig("Texts"));

Noises noises = new(configurator.XElementOfConfig("Noises"));
stateMachine.noises = noises;

Screen screen = new(configurator.XElementOfConfig("I2Cscreen"));
stateMachine.screen = screen;

GPIO gpio = new(configurator.XElementOfConfig("PinsIO"));
stateMachine.gpio = gpio;

RC522 rC522 = new(configurator.XElementOfConfig("ReaderRFID"));
stateMachine.rC5221 = rC522;

AccessControlDb ACDbC = new(configurator.XElementOfConfig("SQL"));
stateMachine.accessControlDb = ACDbC;

Console.ReadKey();




