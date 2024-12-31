using System;
using Godot;
using Microsoft.Scripting.Hosting;

namespace Glython;

public partial class Debug : Control
{
	[Signal]
	public delegate void DebugSignalEventHandler(string message);
	
	private ScriptEngine _engine = null!;
	private ScriptScope _scope = null!;
	
	private void TriggerSignal(string message)
	{
		EmitSignal(SignalName.DebugSignal, message);
	}
	
	private void HandleSignal(string message)
	{
		GD.Print($"Signal received! {message}");
	}
	
	public override void _Ready()
	{
		DebugSignal += HandleSignal;
		
		_engine = IronPython.Hosting.Python.CreateEngine();
		_scope = _engine.CreateScope();
		_scope.SetVariable("signal", TriggerSignal);
		
	
		GD.Print("Debug script loaded!");
		_engine.Execute("signal('hello world')", _scope);
		GD.Print("Test!");
	}
}
