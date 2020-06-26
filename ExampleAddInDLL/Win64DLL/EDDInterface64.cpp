#include "stdafx.h"  
#include "windows.h"  
#include "Objbase.h"
#include "EDDInterface64.h"
#include "stdio.h"
#include "strsafe.h"
#include <iostream>
#include <iomanip>
#include <fstream>

// STRINGs are in Unicode - this is a unicode DLL. 

using namespace std;

void WriteASCII(const char* str)		// ASCII file..
{
	fstream fstream("c:\\code\\eddif.txt", ios::app);         // open the file
	fstream << str;
	fstream.close();
}

void WriteUnicode(LPCTSTR buffer)
{
	char buffer2[30000];
	WideCharToMultiByte(CP_ACP, 0, buffer, -1, buffer2, sizeof(buffer2), 0, 0);		// need to convert back to ASCII
	WriteASCII((const char*)buffer2);
}

void WriteJournalEntry(JournalEntry ptr)
{
	if (ptr.indexno == -1)
	{
		WriteASCII("Journal entry is invalid\n");
		return;
	}

	int const arraysize = 100000;
	TCHAR buffer[arraysize];
	size_t cbDest = arraysize * sizeof(TCHAR);

	if (ptr.ver == 1)
	{
		LPCTSTR pszFormat = TEXT("V%d : %s: %d:%s :'%s' '%s' '%s' : sys %s\nx%f y%f z%f | td%f ts%u | %d %d | loc '%s' st '%s' gm %s grp %s | %d cr | jid %d rec %d\n");
		StringCbPrintfW(buffer, cbDest, pszFormat,
			ptr.ver,
			ptr.utctime, ptr.indexno, ptr.eventid,
			ptr.name, ptr.info, ptr.detailedinfo,
			ptr.systemname,
			ptr.x, ptr.y, ptr.z, ptr.travelleddistance, ptr.travelledseconds, ptr.islanded ? 1 : 0, ptr.isdocked ? 1 : 0, ptr.whereami, ptr.shiptype, ptr.gamemode, ptr.group, ptr.credits,
			ptr.jid, ptr.totalrecords);
	}
	else
	{
		LPCTSTR pszFormat = TEXT("V%d : %s: %d:%s :'%s' '%s' '%s' : sys %s\nx%f y%f z%f | td%f ts%u | %d %d | loc '%s' st '%s' gm %s grp %s | %d cr | jid %d rec %d\nJSON:%s\nCmdr %s\n");
		StringCbPrintfW(buffer, cbDest, pszFormat,
			ptr.ver,
			ptr.utctime, ptr.indexno, ptr.eventid,
			ptr.name, ptr.info, ptr.detailedinfo,
			ptr.systemname,
			ptr.x, ptr.y, ptr.z, ptr.travelleddistance, ptr.travelledseconds, ptr.islanded ? 1 : 0, ptr.isdocked ? 1 : 0, ptr.whereami, ptr.shiptype, ptr.gamemode, ptr.group, ptr.credits,
			ptr.jid, ptr.totalrecords , ptr.json, ptr.cmdrname);
	}

	//int of = offsetof(NewJournalEntry, whereami);
	//StringCbPrintfW(buffer, cbDest, L"%d", of);

	wcscat_s(buffer, arraysize, L"Materials:");

	if (ptr.materials.cDims == 1)
	{

		if ((ptr.materials.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.materials, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.materials, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.materials, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}
	wcscat_s(buffer, arraysize, L"\n");
	wcscat_s(buffer, arraysize, L"Commodities:");

	if (ptr.commodities.cDims == 1)
	{
		if ((ptr.commodities.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.commodities, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.commodities, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.commodities, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);		// WORDs! length
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}

	wcscat_s(buffer, arraysize, L"\n");
	wcscat_s(buffer, arraysize, L"Missions:");

	if (ptr.currentmissions.cDims == 1)
	{
		if ((ptr.currentmissions.fFeatures & FADF_BSTR) == FADF_BSTR)
		{
			BSTR* bstrArray;
			HRESULT hr = SafeArrayAccessData(&ptr.currentmissions, (void**)&bstrArray);

			long iMin = 0;
			SafeArrayGetLBound(&ptr.currentmissions, 1, &iMin);
			long iMax = 0;
			SafeArrayGetUBound(&ptr.currentmissions, 1, &iMax);

			for (long i = iMin; i <= iMax; ++i)
			{
				wcscat_s(buffer, arraysize, bstrArray[i]);		// WORDs! length
				wcscat_s(buffer, arraysize, L",");		// WORDs! length
			}
		}
	}

	wcscat_s(buffer, arraysize, L"\n");

	WriteASCII("--------------- Journal Entry Data\n");
	WriteUnicode(buffer);
	WriteASCII("--------------- END \n");
}

EDDCallBacks callbacks;

EDD_API BSTR EDDInitialise(BSTR ver, BSTR folder, EDDCallBacks pcallbacks)
{
	WriteASCII("\n\n============================\nInitialise:");
	WriteUnicode(ver);
	WriteASCII(",");
	WriteUnicode(folder);
	WriteASCII("\n");
	callbacks = pcallbacks;
	//MessageBoxW(0, ver, L"Caption ɑːkɒn", MB_OK);
	return SysAllocString(L"0.1.2.3");
	//return SysAllocString(L"!Error is this");		// for an error
}

EDD_API void EDDRefresh(BSTR Commander, JournalEntry ptr)
{
	int const arraysize = 3000;

	TCHAR buffer[arraysize];
	wcscpy_s(buffer, arraysize, L"Refresh:");
	wcscat_s(buffer, arraysize, Commander);
	wcscat_s(buffer, arraysize, L"\n");
	WriteUnicode(buffer);
	WriteJournalEntry(ptr);
}


EDD_API void EDDNewJournalEntry(JournalEntry ptr)
{
	WriteASCII("NewJournalEntry:\n");
	WriteJournalEntry(ptr);
}

EDD_API void EDDNewUIEvent(BSTR str)
{
	WriteASCII("NewUIEvent:\n");
	WriteUnicode(str);
}


EDD_API void EDDTerminate()
{
	WriteASCII("Terminate\n");
}

EDD_API BSTR EDDActionCommand(BSTR action, SAFEARRAY& args)		// should always return a string
{
	int const arraysize = 600;
	TCHAR buffer[arraysize];
	size_t cbDest = arraysize * sizeof(TCHAR);

	*buffer = 0;

	long iMin = 0;
	SafeArrayGetLBound(&args, 1, &iMin);
	long iMax = 0;
	SafeArrayGetUBound(&args, 1, &iMax);

	StringCbPrintfW(buffer, cbDest, L"Action Command %s: %d %d: ", action, iMin, iMax);

	BSTR* bstrArray;
	HRESULT hr = SafeArrayAccessData(&args, (void**)&bstrArray);

	for (long i = iMin; i <= iMax; ++i)
	{
		wcscat_s(buffer, arraysize, bstrArray[i]);
		wcscat_s(buffer, arraysize, L",");		// WORDs! length
	}

	WriteUnicode(buffer);
	WriteASCII("\n");

	if (wcscmp(action, L"HISTORY") == 0)
	{
		if (callbacks.RequestHistory != NULL)
		{
			for (int i = 0; i < 5; i++)
			{
				WriteASCII("call back:\n");
				SAFEARRAY sa;
				JournalEntry je = { 1,1,NULL,NULL,NULL,NULL,sa,sa, NULL,0,0,0, 0,0, 0,0, NULL,NULL,NULL,NULL, 0, NULL, sa };
				(*callbacks.RequestHistory)(i, false, &je);		// perform a call back..
				WriteJournalEntry(je);
			}
		}
	}

	if (wcscmp(action, L"PROGRAM") == 0 && iMax == 1)
	{
		if (callbacks.RunAction != NULL)
		{
			WriteASCII("Run Action:\n");
			callbacks.RunAction(bstrArray[0], bstrArray[1]);
		}
	}

	return SysAllocString(L"+DLL Return value");
}

EDD_API void EDDActionJournalEntry(JournalEntry ptr)
{
	TCHAR buffer[1000];
	wcscpy_s(buffer, 1000, L"Journal Entry sent:\n");
	WriteUnicode(buffer);
	WriteJournalEntry(ptr);
}

