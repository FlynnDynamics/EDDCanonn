// EDD Interface
#pragma once  

#ifdef _USRDLL 
#define EDD_API __declspec(dllexport)   
#else  
#define EDD_API __declspec(dllimport)   
#endif  


extern "C"
{
#pragma pack(show)
	struct JournalEntry
	{
	public:

		int ver;		// version of this structure. Order critical.
		int indexno;	// event index.  If -1, invalid Journal Entry.  Can happen for EDDRefresh if no history
						// 1 = first entry, to totalrecords

		BSTR utctime;
		BSTR name;
		BSTR info;
		BSTR detailedinfo;
		SAFEARRAY& materials;		// BSTRs in the format name:count
		SAFEARRAY& commodities;		// BSTRs in the format name:count

		BSTR systemname;
		double x;
		double y;
		double z;

		double travelleddistance;
		long travelledseconds;

		bool islanded;
		bool isdocked;

		BSTR whereami;
		BSTR shiptype;
		BSTR gamemode;
		BSTR group;
		long credits;

		BSTR eventid;

		SAFEARRAY& currentmissions;		// BSTRs listing current mission details

		long long jid;					// jid of event
		long totalrecords;				// number of records 

		// Version 1 ends

		BSTR json;
		BSTR cmdrname;
		BSTR cmdrfid;
		BSTR shipident;
		BSTR shipname;
		long hullvalue;		
		long rebuy;
		long modulesvalue;		
		bool stored;

		// Version 2 ends

		BSTR travelstate;
		SAFEARRAY& microresources;		// BSTRs listing current mr details

		// Version 3 ends
	};

	// request history.  if isjid=false, 1 = first entry, to end entry.  If isjid=true, its the jid number.
	// Returns true if history entry found, with je set to the journal entry.
	// Returns false if history entry not found, with je set to null
	typedef bool (*EDDRequestHistory)(long index, bool isjid, JournalEntry *je);

	// action a user defined event, of name eventname, thru the EDD event system. Use an EVENT <eventname> statement in your action script
	// to pick the event up and send it to an action program.  You can pass in a list of variables for the action program in the second string
	// use unique names, patterned after your DLL name.  
	// You may issue standard events, but you'll have to make sure all of the multitude of variables are set for other packs to use them properly.
	typedef bool(*EDDRunAction)(BSTR eventname, BSTR parameters); // parameters in format v="k",v2="k" or an empty string

	// Request the ship json loadout

	typedef bool(*EDDGetShipLoadout)(BSTR shipnameordefault); // empty string = current ship, or ship ident, or ship type

	struct EDDCallBacks
	{
		int ver;			// version of this structure . Order critical.
		EDDRequestHistory RequestHistory;		// may be null - check
		EDDRunAction RunAction;					// may be null - check
		// Version 1 ends

		EDDGetShipLoadout GetShipLoadout;

		// Version 2 ends
	};

	// Anthing passed in COPY it, don't ref it, c# may remove them after the return at any time.
	// BSTR return values: once passed back, c# owns the BSTR.  c# insists you use SysAllocString to make the BSTR
	// DO not pass back any global variables.  Do not use the variable after being passed back.
	// It may deallocate at any time.  Best to make a unique copy of the string using SysAllocString
	
	// Called with EDD version A.B.C.D, return NULL if can't operate, or your version as X.Y.Z.B, or !errorstring to say your unhappy (! indicates error)
	EDD_API BSTR EDDInitialise(BSTR ver, BSTR dllfolder, EDDCallBacks requestcallback);		// mandatory

	// optional, last_je is the last one received. last_je.indexno =-1 if no history is present.
	EDD_API void EDDRefresh(BSTR cmdr, JournalEntry last_je);			
	
	// optional. nje will always be set.  Called when a new Journal Entry received
	EDD_API void EDDNewJournalEntry(JournalEntry nje);

	// optional. 
	EDD_API void EDDNewUIEvent(BSTR json);		// see EliteDangerousCore::UI for json fields.

	// optional. Called at EDD termination
	EDD_API void EDDTerminate();		
							
	// optional. Called by Action DLLCall. Args could be an empty array.
	// Return !errorstring if an error occurs, or +resultstring if all okay (which is placed in Action variable DLLResult).  Do not pass back null
	EDD_API BSTR EDDActionCommand(BSTR action, SAFEARRAY& args);		
	
	// optional. Called by Action DLLCall to feed a journal entry to you.
	EDD_API void EDDActionJournalEntry(JournalEntry je);		
}

