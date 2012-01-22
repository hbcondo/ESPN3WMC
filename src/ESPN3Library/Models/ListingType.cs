using System;

namespace ESPN3Library.Models
{
	/// <summary>
	/// An enumeration of event listing types
	/// </summary>
	public enum ListingType
	{
		/// <summary>
		/// Live stream of event
		/// </summary>
		LIVE,
		
		/// <summary>
		/// Taped event
		/// </summary>
		REPLAY,

		/// <summary>
		/// Event that is about to be playable
		/// </summary>
		UPCOMING,

		/// <summary>
		/// Event will almost stream live
		/// </summary>
		ALMOSTLIVE,

		/// <summary>
		/// Replay of some sort
		/// </summary>
		NEXDEFREPLAY,

		/// <summary>
		/// Unknown listing type
		/// </summary>
		UNKNOWN = 99
	}
}
