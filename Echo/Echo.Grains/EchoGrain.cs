using Echo.Contracts;
using Echo.Contracts.Messages;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Echo.Grains
{
   public sealed class EchoGrain
	   : Grain, IEchoGrain
   {
	  private int _callCounter = 0;
	  private uint _msgRepeatCounter = 0;

	  public async Task SpeakAsync(EchoSpeakMessage message)
	  {
		 if (message == null) { throw new ArgumentNullException(nameof(message)); }

		 if (CanShowCounter()) { await Console.Out.WriteLineAsync($"Grain has spoken {_callCounter} times and has repeated {_msgRepeatCounter} messages."); }

		 for (var i = 0; i < message.Repeat; i++)
		 {
			await Console.Out.WriteLineAsync(
				$"{i} - {message.Message}");
		 }

		 UpdateCallCount(message.Repeat);
	  }

	  private void UpdateCallCount(uint repeatCount = 0)
	  {
		 _callCounter++;
		 _msgRepeatCounter += repeatCount;
	  }

	  private bool CanShowCounter()
	  {
		 return ((_callCounter != 0) && ((_callCounter % 2) == 0));
	  }
   }
}
