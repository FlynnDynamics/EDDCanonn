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

		int ver;		// version of this structure = 1. Order critical.
		int indexno;	// event index.  If -1, invalid Journal Entry.  Can happen for EDDRefresh if no history

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

		SAFEARRAY& currentmissions;		// BSTRs listing current missions
	};

	struct Fred
	{
		int a;
		BSTR f;
		SAFEARRAY& currentmissions;		// BSTRs listing current missions
	};

	typedef void (*EDDRequestHistory)(long index, bool isjid, JournalEntry *f);		// if idjid=0, then 0 = last entry, onwards.

	struct EDDCallBacks
	{
		int ver;
		EDDRequestHistory historycallback;
	};

	// All complex structures - use them immediately, c# may remove them after the return at any time.

	// Called with EDD version A.B.C.D, return NULL if can't operate, or your version as X.Y.Z.B
	EDD_API BSTR EDDInitialise(BSTR ver, BSTR dllfolder, EDDCallBacks requestcallback);		// mandatory
	EDD_API void EDDRefresh(BSTR cmdr, JournalEntry last_je);			// optional, last_je is the last one received
	EDD_API void EDDNewJournalEntry(JournalEntry nje);		// optional. nje will always be set.  Called when a new Journal Entry received
	EDD_API void EDDTerminate();					// optional

	EDD_API BSTR EDDActionCommand(BSTR action, SAFEARRAY& args);		// optional. If implemented, always return string. Args could be an empty array. Called by Action
	EDD_API void EDDActionJournalEntry(JournalEntry je);		// optional. Called by Action to feed a journal entry to you.

	EDD_API void EDDRequestedHistory(JournalEntry je);		// optional. Due to the DLL asking for history

}

